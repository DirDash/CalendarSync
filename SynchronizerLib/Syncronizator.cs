using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public class Syncronizator
    {
        private DifferenceFinder _differenceFinder;

        public void ApplyAllUpdates(DateTime startDate, DateTime finishDate, List<ICalendarService> calendars)
        {
            List<List<SynchronEvent>> MeetingsInTheCalendars = new List<List<SynchronEvent>>();
            
            foreach(var currentCalendar in calendars)
                MeetingsInTheCalendars.Add(new EventsSiever().SieveEventsOnPeriodOfTime(startDate, finishDate, currentCalendar.GetAllItems(startDate, finishDate)));

            if (_differenceFinder == null)
                _differenceFinder = new DifferenceFinder();
            for (int i = 0; i < calendars.Count;++i)
            {
                for (int j = 0; j < calendars.Count; ++j)
                {
                    if (i == j)
                        continue;
                    OneWaySync(calendars[i], MeetingsInTheCalendars[j], MeetingsInTheCalendars[i]);
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
