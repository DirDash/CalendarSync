using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public class Synchronizer
    {
        private DifferenceFinder _differenceFinder;
        public List<ICalendarService> Calendars { get; private set; }

        public Synchronizer()
        {
            Calendars = new List<ICalendarService>();
        }

        public Synchronizer(List<ICalendarService> calendars)
        {
            Calendars = new List<ICalendarService>();
            foreach (var calendar in calendars)
                Calendars.Add(calendar);
            _differenceFinder = new DifferenceFinder();
        }

        public void AddCalendar(ICalendarService newCalnedar)
        {
            Calendars.Add(newCalnedar);
        }

        public void RemoveCalendar(ICalendarService calendarToRemove)
        {
            Calendars.Remove(calendarToRemove);
        }

        public virtual void Synchronize(DateTime startDate, DateTime finishDate)
        {
            //throw new NullReferenceException(); // for testing
            List<List<SynchronEvent>> MeetingsInTheCalendars = new List<List<SynchronEvent>>();
            
            foreach (var currentCalendar in Calendars)
                MeetingsInTheCalendars.Add(new EventsSiever().SieveEventsOnPeriodOfTime(startDate, finishDate, currentCalendar.GetAllItems(startDate, finishDate)));
                        
            for (int i = 0; i < Calendars.Count;++i)
            {
                for (int j = 0; j < Calendars.Count; ++j)
                {
                    if (i == j)
                        continue;
                    OneWaySync(Calendars[i], MeetingsInTheCalendars[j], MeetingsInTheCalendars[i]);
                }
            }
        }

        private void OneWaySync(ICalendarService targetCalendarService, List<SynchronEvent> sourceMeetings, List<SynchronEvent> targetMeetings)
        {
            var nonExistInTarget = _differenceFinder.GetDifferenceToPush(sourceMeetings, targetMeetings);
            var needToDeleteInTarget = _differenceFinder.GetDifferenceToDelete(targetMeetings, sourceMeetings);
            var needToUpdateInTarget = _differenceFinder.GetDifferenceToUpdate(targetMeetings, sourceMeetings);

            targetCalendarService.PushEvents(nonExistInTarget);
            targetCalendarService.DeleteEvents(needToDeleteInTarget);
            targetCalendarService.UpdateEvents(needToUpdateInTarget);
        }
    }
}
