using System;
using System.Collections.Generic;
using SynchronizerLib.SynchronEvents;
using SynchronizerLib.CalendarServices;

namespace SynchronizerLib
{
    public class Synchronizer : ISynchronizer
    {
        private DifferenceFinder _differenceFinder;
        private EventsSiever _eventSiever;
        private EventTransformer _eventTransformer;

        public Synchronizer()
        {
            _differenceFinder = new DifferenceFinder();
            _eventSiever = new EventsSiever();
            _eventTransformer = new EventTransformer();
        }

        public void SynchronizeAll(CalendarStore calendarStore, DateTime startDate, DateTime finishDate)
        {
            calendarStore.RefreshSyncRuleForAllCalendars();
            var calendarList = calendarStore.GetCalendars();
            List<List<SynchronEvent>> eventsInTheCalendars = new List<List<SynchronEvent>>();

            foreach (var currentCalendar in calendarList)
            {
                var events = currentCalendar.GetAllItems(startDate, finishDate);
                events = _eventSiever.Sieve(events, currentCalendar.ConfigManager.OutFilter);
                events = _eventTransformer.Transform(events, currentCalendar.ConfigManager.OutTransformation);
                eventsInTheCalendars.Add(events);
            }
                        
            for (int i = 0; i < calendarList.Count; ++i)
            {
                for (int j = 0; j < calendarList.Count; ++j)
                {
                    if (calendarStore.SyncIsAllowed(calendarList[i], calendarList[j]))
                        OneWaySync(calendarList[j], eventsInTheCalendars[i], eventsInTheCalendars[j]);
                }
            }
        }

        private void OneWaySync(ICalendarService targetCalendarService, List<SynchronEvent> sourceEvents, List<SynchronEvent> targetEvents)
        {
            var nonExistInTarget = _differenceFinder.GetDifferenceToPush(sourceEvents, targetEvents);
            var needToUpdateInTarget = _differenceFinder.GetDifferenceToUpdate(sourceEvents, targetEvents);
            var needToDeleteInTarget = _differenceFinder.GetDifferenceToDelete(sourceEvents, targetEvents);

            nonExistInTarget = _eventTransformer.Transform(nonExistInTarget, targetCalendarService.ConfigManager.InTransformation);
            targetCalendarService.PushEvents(nonExistInTarget);

            needToUpdateInTarget = _eventTransformer.Transform(needToUpdateInTarget, targetCalendarService.ConfigManager.InTransformation);
            targetCalendarService.UpdateEvents(needToUpdateInTarget);
            
            targetCalendarService.DeleteEvents(needToDeleteInTarget);           
        }
    }
}
