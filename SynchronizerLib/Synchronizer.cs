using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public class Synchronizer
    {
        private DifferenceFinder _differenceFinder;
        private List<ICalendarService> _calendars;

        public Synchronizer(List<ICalendarService> calendars)
        {
            _calendars = new List<ICalendarService>();
            foreach (var calendar in calendars)
                _calendars.Add(calendar);
        }

        public virtual void Synchronize(DateTime startDate, DateTime finishDate)
        {
            List<List<SynchronEvent>> MeetingsInTheCalendars = new List<List<SynchronEvent>>();
            
            foreach (var currentCalendar in _calendars)
                MeetingsInTheCalendars.Add(new EventsSiever().SieveEventsOnPeriodOfTime(startDate, finishDate, currentCalendar.GetAllItems(startDate, finishDate)));

            if (_differenceFinder == null)
                _differenceFinder = new DifferenceFinder();
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
            var needToDeleteInTarget =
                _differenceFinder.GetDifferenceToDelete(targetMeetings, sourceMeetings);
            var needToUpdateInTarget = _differenceFinder.GetDifferenceToUpdate(targetMeetings, sourceMeetings);

            targetCalendarService.PushEvents(nonExistInTarget);
            targetCalendarService.DeleteEvents(needToDeleteInTarget);
            targetCalendarService.UpdateEvents(needToUpdateInTarget);
        }
    }
}
