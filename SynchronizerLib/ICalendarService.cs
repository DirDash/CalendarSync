using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public interface ICalendarService
    {
        void PushEvents(List<SynchronEvent> events);
        
        void DeleteEvents(List<SynchronEvent> events);

        List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime);

        void UpdateEvents(List<SynchronEvent> needToUpdate);
    }
}
