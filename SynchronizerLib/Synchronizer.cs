using System;
using System.Collections.Generic;
using System.Linq;

namespace SynchronizerLib
{
    public class Synchronizer : ISynchronizer
    {
        private DifferenceFinder _differenceFinder;

        public Synchronizer()
        {
            _differenceFinder = new DifferenceFinder();
        }

        public void SynchronizeAll(IEnumerable<ICalendarService> calendars, DateTime startDate, DateTime finishDate)
        {
            var calendarList = calendars.ToList();
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
                    if (i == j)
                        continue;
                    OneWaySync(calendarList[i], MeetingsInTheCalendars[j], MeetingsInTheCalendars[i]);
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
