using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using SynchronizerLib.SynchronEvents;
using SynchronizerLib.CalendarServices;

namespace SynchronizerLib.Google
{
    public class GoogleService : ICalendarService
    {
        private string _serviceName = "google";
        private GoogleAPIGateway _APIGateway = new GoogleAPIGateway();
        private GoogleEventConverter _converter = new GoogleEventConverter();
        private CalendarServiceConfigManager _configManager = new CalendarServiceConfigManager("googleServiceSettings");        

        public CalendarServiceConfigManager ConfigManager
        {
            get { return _configManager; }
        }

        public string ServiceName
        {
            get { return _serviceName; }
        }

        public override string ToString()
        {
            return "Google Calendar Service";
        }

        public List<SynchronEvent> GetAllItems(DateTime startDate, DateTime finishDate)
        {
            var allItems = new List<SynchronEvent>();
            foreach (var item in _APIGateway.GetAllItems(startDate, finishDate).Items)
                allItems.Add(_converter.ConvertToSynchronEvent(item));
            return allItems;
        }

        public string GetName()
        {
            return _serviceName;
        }

        public void PushEvents(List<SynchronEvent> events)
        {
            var googleEventList = new List<Event>();
            foreach (var synchronEvent in events)
                googleEventList.Add(_converter.ConvertToGoogleEvent(synchronEvent));
            _APIGateway.PushEvents(googleEventList);           
        }

        public void DeleteEvents(List<SynchronEvent> events)
        {
            _APIGateway.DeleteEvents(events);
        }        

        public void UpdateEvents(List<SynchronEvent> events)
        {
            _APIGateway.UpdateEvents(events);
        }        
    }
}
