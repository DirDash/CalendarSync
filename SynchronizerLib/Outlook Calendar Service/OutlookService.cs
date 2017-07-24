using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;

namespace SynchronizerLib
{
    public class OutlookService : ICalendarService
    {
        private string _serviceName = "outlook";
        private Application _oApp = null;
        private NameSpace _mapiNamespace = null;
        private MAPIFolder _calendarFolder = null;
        private Items _outlookCalendarItems = null;
        private DateTime _minTime;
        private DateTime _maxTime;
        //private bool _alreadyInit = false;
        private OutlookEventConverter _converter = new OutlookEventConverter();
        private CalendarServiceConfigManager _configManager = new CalendarServiceConfigManager("outlookServiceSettings");

        private string GetDateInString(DateTime curDate)
        {
            string result = "";
            result += curDate.Day.ToString() + "/" +curDate.Month.ToString() + "/" + curDate.Year.ToString();
            result += " " + curDate.Hour.ToString() + ":" + curDate.Minute.ToString();
            return result;
        }

        private void InitOutlookService()
        {
            //if (!_alreadyInit)
            {
                _oApp = new Application();
                _mapiNamespace = _oApp.GetNamespace("MAPI");
                _calendarFolder = _mapiNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
                _outlookCalendarItems = _calendarFolder.Items;

                _outlookCalendarItems.Sort("[Start]");
                _outlookCalendarItems.IncludeRecurrences = true;

                string s1 = GetDateInString(_minTime);
                string s2 = GetDateInString(_maxTime);
                var filterString = "[Start] >= '" + s1 + "' AND [End] < '" + s2 + "'";
                _outlookCalendarItems = _outlookCalendarItems.Restrict(filterString);
                _converter = new OutlookEventConverter();
                //_alreadyInit = true;
            }
        }

        public List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime)
        {
            var resultList = new List<SynchronEvent>();
            _minTime = startTime.ToUniversalTime();

            _minTime = _minTime.AddHours(-_minTime.Hour);
            _minTime = _minTime.AddMinutes(-_minTime.Minute);
            _minTime = _minTime.AddSeconds(-_minTime.Second);
            _minTime = _minTime.AddMilliseconds(-_minTime.Millisecond - 1);

            _maxTime = finishTime.ToUniversalTime();
            InitOutlookService();

            foreach (AppointmentItem item in _outlookCalendarItems)
            {
                if (item.Start > finishTime)
                    break;
                resultList.Add(_converter.ConvertToSynchronEvent(item));
            }
            return resultList;
        }

        public IEnumerable<string> GetFilters()
        {            
            return new List<string> { _configManager.OutFilter };
        }

        public IEnumerable<EventTransformation> GetOutTransformations()
        {
            return new List<EventTransformation> { _configManager.OutTransformation };
        }

        public IEnumerable<EventTransformation> GetInTransformations()
        {
            return new List<EventTransformation> { _configManager.InTransformation };
        }

        public IEnumerable<string> GetBannedToSyncToServices()
        {
            return _configManager.BannedToSyncToServices;
        }

        public CalendarServiceConfigManager GetConfigManager()
        {
            return _configManager;
        }

        public string GetName()
        {
            return _serviceName;
        }

        public void PushEvents(List<SynchronEvent> events)
        {
            InitOutlookService();
            foreach (var eventToPush in events)
            {
                var current = _converter.ConvertToOutlookEvent(eventToPush);
                current.Save();   
            }            
        }

        public void DeleteEvents(List<SynchronEvent> events)
        {
            InitOutlookService();

            foreach (AppointmentItem item in _outlookCalendarItems)
            {
                if (item.Start > _maxTime)
                    break;
                if (string.IsNullOrEmpty(item.Mileage))
                    continue;
                foreach (var eventToDelete in events)
                {
                    if(item.Mileage == eventToDelete.GetId())
                        item.Delete();
                }
            }
        }

        public void UpdateEvents(List<SynchronEvent> needToUpdate)
        {
            InitOutlookService();

            foreach (AppointmentItem item in _outlookCalendarItems)
            {
                if (item.Start > _maxTime)
                    break;
                if (string.IsNullOrEmpty(item.Mileage))
                    continue;
                foreach (var eventToUpdate in needToUpdate)
                {
                    if (item.Mileage == eventToUpdate.GetId())
                    {
                        string buf = "";
                        List<string> AllParticipants = eventToUpdate.GetParticipants();

                        for (int i = 0; i < AllParticipants.Count; ++i)
                        {
                            if (i + 1 < AllParticipants.Count)
                                buf = buf + AllParticipants[i] + "; ";
                            else
                                buf = buf + AllParticipants[i];
                        }
                        item.RequiredAttendees = buf;
                        item.Subject = eventToUpdate.GetSubject();
                        item.StartUTC = eventToUpdate.GetStartUTC();
                        item.EndUTC = eventToUpdate.GetFinishUTC();
                        item.Body = eventToUpdate.GetDescription();
                        item.Location = eventToUpdate.GetLocation();
                        item.Save();
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Outlook Calendar Service";
        }
    }
}
