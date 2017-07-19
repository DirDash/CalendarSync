using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;

namespace SynchronizerLib
{
    public class OutlookService : ICalendarService
    {
        private Application oApp = null;
        private NameSpace mapiNamespace = null;
        private MAPIFolder calendarFolder = null;
        private Items outlookCalendarItems = null;
        private DateTime minTime;
        private DateTime maxTime;
        private bool ifAlreadyInit = false;
        private OutlookEventConverter _converter;

        private string GetDateInString(DateTime curDate)
        {
            string result = "";
            result += curDate.Day.ToString() + "/" +curDate.Month.ToString() + "/" + curDate.Year.ToString();
            result += " " + curDate.Hour.ToString() + ":" + curDate.Minute.ToString();
            return result;
        }

        private void InitOutlookService()
        {
            //if (!ifAlreadyInit)
            {
                oApp = new Application();
                mapiNamespace = oApp.GetNamespace("MAPI");
                calendarFolder = mapiNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
                outlookCalendarItems = calendarFolder.Items;

                outlookCalendarItems.Sort("[Start]");
                outlookCalendarItems.IncludeRecurrences = true;

                string s1 = GetDateInString(minTime);
                string s2 = GetDateInString(maxTime);
                var filterString = "[Start] >= '" + s1 + "' AND [End] < '" + s2 + "'";
                outlookCalendarItems = outlookCalendarItems.Restrict(filterString);
                _converter = new OutlookEventConverter();
                ifAlreadyInit = true;
            }
        }

        public List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime)
        {
            var resultList = new List<SynchronEvent>();
            minTime = startTime.ToUniversalTime();

            minTime = minTime.AddHours(-minTime.Hour);
            minTime = minTime.AddMinutes(-minTime.Minute);
            minTime = minTime.AddSeconds(-minTime.Second);
            minTime = minTime.AddMilliseconds(-minTime.Millisecond - 1);

            maxTime = finishTime.ToUniversalTime();
            InitOutlookService();

            foreach (AppointmentItem item in outlookCalendarItems)
            {
                if (item.Start > finishTime)
                    break;
                resultList.Add(_converter.ConvertToSynchronEvent(item));
            }
            return resultList;
        }

        public IEnumerable<string> GetFilters()
        {
            var filters = new List<string>();
            filters.Add(SynchronizationConfigManager.OutlookFilter);
            return filters;
        }

        public IEnumerable<EventTransformation> GetOutTransformations()
        {
            var outTransformations = new List<EventTransformation>();
            outTransformations.Add(SynchronizationConfigManager.OutlookOutTransformation);
            return outTransformations;
        }

        public IEnumerable<EventTransformation> GetInTransformations()
        {
            var inTransformations = new List<EventTransformation>();
            inTransformations.Add(SynchronizationConfigManager.OutlookInTransformation);
            return inTransformations;
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

            foreach (AppointmentItem item in outlookCalendarItems)
            {
                if (item.Start > maxTime)
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

            foreach (AppointmentItem item in outlookCalendarItems)
            {
                if (item.Start > maxTime)
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
