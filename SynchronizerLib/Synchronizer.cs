using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public class Synchronizer : ISynchronizer
    {
        private DifferenceFinder _differenceFinder;
        private List<ICalendarService> _calendars;

        public Synchronizer(List<ICalendarService> calendars)
        {
            _calendars = calendars;
            _differenceFinder = new DifferenceFinder();
        }

        public void Synchronize(DateTime startDate, DateTime finishDate)
        {
            List<List<SynchronEvent>> MeetingsInTheCalendars = new List<List<SynchronEvent>>();
            
            foreach (var currentCalendar in _calendars)
                MeetingsInTheCalendars.Add(new EventsSiever().SieveEventsOnPeriodOfTime(startDate, finishDate, currentCalendar.GetAllItems(startDate, finishDate)));
                        
            for (int i = 0; i < _calendars.Count;++i)
            {
                for (int j = 0; j < _calendars.Count; ++j)
                {
                    if (i == j)
                        continue;
                    OneWaySync(_calendars[i], MeetingsInTheCalendars[j], MeetingsInTheCalendars[i]);
                }
            }
        }

        private void OneWaySync(ICalendarService targetCalendarService, List<SynchronEvent> sourceMeetings, List<SynchronEvent> targetMeetings)
        {
            var nonExistInTarget = _differenceFinder.GetDifferenceToPush(sourceMeetings, targetMeetings);            
            var needToUpdateInTarget = _differenceFinder.GetDifferenceToUpdate(targetMeetings, sourceMeetings);            
            var needToDeleteInTarget = _differenceFinder.GetDifferenceToDelete(targetMeetings, sourceMeetings); 
            
            targetCalendarService.DeleteEvents(needToDeleteInTarget);
            targetCalendarService.PushEvents(nonExistInTarget);
            targetCalendarService.UpdateEvents(needToUpdateInTarget);
        }
    }
}
