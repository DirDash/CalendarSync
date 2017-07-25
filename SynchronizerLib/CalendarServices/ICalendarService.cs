using System;
using System.Collections.Generic;
using SynchronizerLib.SynchronEvents;

namespace SynchronizerLib.CalendarServices
{
    public interface ICalendarService
    {
        string ServiceName { get; }

        CalendarServiceConfigManager ConfigManager { get; }

        List<SynchronEvent> GetAllItems(DateTime startDate, DateTime finishDate);

        void PushEvents(List<SynchronEvent> events);
        
        void DeleteEvents(List<SynchronEvent> events);       

        void UpdateEvents(List<SynchronEvent> events);
    }
}
