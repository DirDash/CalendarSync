using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public class Synchronizer : ISynchronizer
    {
        private DifferenceFinder _differenceFinder;

        public Synchronizer()
        {
            _differenceFinder = new DifferenceFinder();
        }

        public void SynchronizeAll(CalendarStore calendarStore, DateTime startDate, DateTime finishDate)
        {
            var calendarList = calendarStore.GetCalendars();
            List<List<SynchronEvent>> MeetingsInTheCalendars = new List<List<SynchronEvent>>();

            foreach (var currentCalendar in calendarList)
            {
                var events = currentCalendar.GetAllItems(startDate, finishDate);
                var filters = currentCalendar.GetFilters();
                // выполнить преобразования
                MeetingsInTheCalendars.Add(new EventsSiever().Sieve(events, filters));
            }
                        
            for (int i = 0; i < calendarList.Count; ++i)
            {
                for (int j = 0; j < calendarList.Count; ++j)
                {
                    if (calendarStore.SyncIsAllowed(calendarList[i], calendarList[j]))
                        OneWaySync(calendarList[j], MeetingsInTheCalendars[i], MeetingsInTheCalendars[j]);
                }
            }
        }

        private void OneWaySync(ICalendarService targetCalendarService, List<SynchronEvent> sourceMeetings, List<SynchronEvent> targetMeetings)
        {
            var nonExistInTarget = _differenceFinder.GetDifferenceToPush(sourceMeetings, targetMeetings);
            var needToUpdateInTarget = _differenceFinder.GetDifferenceToUpdate(targetMeetings, sourceMeetings);
            var needToDeleteInTarget = _differenceFinder.GetDifferenceToDelete(targetMeetings, sourceMeetings);

            targetCalendarService.PushEvents(nonExistInTarget);            
            targetCalendarService.UpdateEvents(needToUpdateInTarget);            
            targetCalendarService.DeleteEvents(needToDeleteInTarget);           
        }
    }
}
