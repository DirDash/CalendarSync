using System;
using SynchronizerLib.SynchronEvents;
using SynchronizerLib.CalendarServices;

namespace SynchronizerLib
{
    public class Synchronizer : ISynchronizer
    {        
        private EventsSiever _eventSiever = new EventsSiever();
        private EventTransformer _eventTransformer = new EventTransformer();
        private DifferenceFinder _differenceFinder = new DifferenceFinder();

        public void Synchronize(CalendarStore calendarStore, DateTime startDate, DateTime finishDate)
        {
            foreach (var sourceCalendar in calendarStore.Calendars)
                foreach (var targetCalendar in calendarStore.Calendars)
                    if (calendarStore.SyncIsAllowed(sourceCalendar, targetCalendar))
                        SynchronizeOneWay(sourceCalendar, targetCalendar, startDate, finishDate);
        }

        private void SynchronizeOneWay(ICalendarService sourceCalendar, ICalendarService targetCalendar, DateTime startDate, DateTime finishDate)
        {
            // Extract
            var sourceEvents = _eventSiever.Sieve(sourceCalendar.GetAllItems(startDate, finishDate), sourceCalendar.ConfigManager.OutFilter);
            var targetEvents = _eventSiever.Sieve(targetCalendar.GetAllItems(startDate, finishDate), targetCalendar.ConfigManager.OutFilter);
            // Transform
            sourceEvents = _eventTransformer.Transform(sourceEvents, sourceCalendar.ConfigManager.OutTransformation);
            var eventsToPush = _differenceFinder.GetDifferenceToPush(sourceEvents, targetEvents);
            var eventsToUpdate = _differenceFinder.GetDifferenceToUpdate(sourceEvents, targetEvents);
            var eventsToDelete = _differenceFinder.GetDifferenceToDelete(sourceEvents, targetEvents);
            eventsToPush = _eventTransformer.Transform(eventsToPush, targetCalendar.ConfigManager.InTransformation);
            eventsToUpdate = _eventTransformer.Transform(eventsToUpdate, targetCalendar.ConfigManager.InTransformation);
            eventsToDelete = _eventTransformer.Transform(eventsToDelete, targetCalendar.ConfigManager.InTransformation);
            // Load
            targetCalendar.PushEvents(eventsToPush);
            targetCalendar.UpdateEvents(eventsToUpdate);
            targetCalendar.DeleteEvents(eventsToDelete);
        }
    }
}
