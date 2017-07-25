using System;
using System.Collections.Generic;
using System.Linq;

namespace SynchronizerLib.CalendarServices
{
    public class CalendarStore
    {
        private List<ICalendarService> _calendars = new List<ICalendarService>();
        private bool[,] _syncMatrix = new bool[0,0];

        public CalendarStore() { }
        
        public CalendarStore(IEnumerable<ICalendarService> calendars)
        {
            foreach (var calendar in calendars)
                _calendars.Add(calendar);
            _syncMatrix = new bool[_calendars.Count, _calendars.Count];
            RefreshSyncRuleForAllCalendars();
        }

        public List<ICalendarService> Calendars
        {
            get { return _calendars; }
        }

        public bool SyncIsAllowed(ICalendarService sourceCalendar, ICalendarService targetCalendar)
        {
            return _syncMatrix[_calendars.IndexOf(sourceCalendar), _calendars.IndexOf(targetCalendar)];
        }

        public void RefreshSyncRuleForAllCalendars()
        {
            for (int i = 0; i < _calendars.Count; i++)
                RefreshSyncRulesFor(i);
        }
        
        private void RefreshSyncRulesFor(int calendarIndex)
        {
            var bannedServices = _calendars[calendarIndex].ConfigManager.BannedToSyncToServices;
            for (int j = 0; j < _calendars.Count; j++)
            {                
                if (j == calendarIndex || bannedServices.Contains(_calendars[j].ServiceName.ToLower()))
                    _syncMatrix[calendarIndex, j] = false;
                else
                    _syncMatrix[calendarIndex, j] = true;
            }
        }
    }
}
