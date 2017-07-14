using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public class CalendarStore
    {
        private List<ICalendarService> _calendars = new List<ICalendarService>();
        private List<List<bool>> _syncMatrix = new List<List<bool>>();

        public CalendarStore() { }
        
        public CalendarStore(IEnumerable<ICalendarService> calendars)
        {
            foreach (var calendar in calendars)
                AddCalendar(calendar);            
        }

        public List<ICalendarService> GetCalendars()
        {
            var calendars = new List<ICalendarService>();
            foreach (var calendar in _calendars)
                calendars.Add(calendar);
            return calendars;
        }

        public void AddCalendar(ICalendarService calendar)
        {
            _calendars.Add(calendar);
            var newCalendarSyncRow = new List<bool>();            
            for (int i = 0; i < _calendars.Count - 1; i++)
                newCalendarSyncRow.Add(true);
            newCalendarSyncRow.Add(false);
            _syncMatrix.Add(newCalendarSyncRow);
            for (int i = 0; i < _syncMatrix.Count - 1; i++)
                _syncMatrix[i].Add(true);
        }

        public void RemoveCalendar(ICalendarService calendar)
        {
            int calendarIndex = _calendars.IndexOf(calendar);
            _calendars.RemoveAt(calendarIndex);
            _syncMatrix.RemoveAt(calendarIndex);
            foreach (var row in _syncMatrix)
                row.RemoveAt(calendarIndex);
        }

        public bool SyncIsAllowed(ICalendarService sourceCalendar, ICalendarService targetCalendar)
        {
            return _syncMatrix[_calendars.IndexOf(sourceCalendar)][_calendars.IndexOf(targetCalendar)];
        }

        public void ChangeSyncMode(ICalendarService sourceCalendar, ICalendarService targetCalendar, bool syncIsAllowed)
        {
            int sourceIndex = _calendars.IndexOf(sourceCalendar);
            int targetIndex = _calendars.IndexOf(targetCalendar);
            if (sourceIndex != targetIndex)
                _syncMatrix[sourceIndex][targetIndex] = syncIsAllowed;
        }        
    }
}
