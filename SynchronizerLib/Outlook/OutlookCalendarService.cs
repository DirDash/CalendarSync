using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;
using SynchronizerLib.SynchronEvents;
using SynchronizerLib.CalendarServices;

namespace SynchronizerLib.Outlook
{
    public class OutlookCalendarService : ICalendarService
    {
        private string _serviceName = "outlook";
        private OutlookAPIGateway _APIGateway = new OutlookAPIGateway();
        private OutlookEventConverter _converter = new OutlookEventConverter();
        private CalendarServiceConfigManager _configManager = new CalendarServiceConfigManager("outlookServiceSettings");        

        public string ServiceName
        {
            get { return _serviceName; }
        }

        public CalendarServiceConfigManager ConfigManager
        {
            get { return _configManager; }
        }

        public override string ToString()
        {
            return "Outlook Calendar Service";
        }

        public List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime)
        {
            var resultList = new List<SynchronEvent>();
            foreach (var item in _APIGateway.GetAllItems(startTime, finishTime))
                resultList.Add(_converter.ConvertToSynchronEvent(item));
            return resultList;    
        }

        public void PushEvents(List<SynchronEvent> events)
        {
            var items = new List<AppointmentItem>();
            foreach (var ev in events)
                items.Add(_converter.ConvertToOutlookEvent(ev));
            _APIGateway.PushEvents(items);         
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
