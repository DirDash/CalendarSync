using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public interface ICalendarService
    {
        List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime);

        List<SieveRule> GetSieveRules();

        void PushEvents(List<SynchronEvent> events);
        
        void DeleteEvents(List<SynchronEvent> events);       

        void UpdateEvents(List<SynchronEvent> needToUpdate);
    }
}
