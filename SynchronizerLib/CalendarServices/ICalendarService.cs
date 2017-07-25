using System;
using System.Collections.Generic;
using SynchronizerLib.Events;

namespace SynchronizerLib.CalendarServices
{
    public interface ICalendarService
    {
       

        IEnumerable<string> GetFilters();

        IEnumerable<EventTransformation> GetOutTransformations();

        IEnumerable<EventTransformation> GetInTransformations();

        IEnumerable<string> GetBannedToSyncToServices();


        string GetName();

        CalendarServiceConfigManager GetConfigManager();



        List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime);

        void PushEvents(List<SynchronEvent> events);
        
        void DeleteEvents(List<SynchronEvent> events);       

        void UpdateEvents(List<SynchronEvent> needToUpdate);
    }
}
