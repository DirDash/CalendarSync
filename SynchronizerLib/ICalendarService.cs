using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public interface ICalendarService
    {
        List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime);

        IEnumerable<string> GetFilters();

        IEnumerable<EventTransformation> GetOutTransformations();

        IEnumerable<EventTransformation> GetInTransformations();

        void PushEvents(List<SynchronEvent> events);
        
        void DeleteEvents(List<SynchronEvent> events);       

        void UpdateEvents(List<SynchronEvent> needToUpdate);
    }
}
