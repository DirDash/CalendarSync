using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;
using SynchronizerLib.SynchronEvents;

namespace SynchronizerLib.Outlook
{
    internal class OutlookAPIGateway
    {
        private Application _oApp = null;
        private NameSpace _mapiNamespace = null;
        private MAPIFolder _calendarFolder = null;
        private Items _outlookCalendarItems = null;
        private DateTime _minTime;
        private DateTime _maxTime;

        private void UpdateCalendarInfo()
        {
            _oApp = new Application();
            _mapiNamespace = _oApp.GetNamespace("MAPI");
            _calendarFolder = _mapiNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
            _outlookCalendarItems = _calendarFolder.Items;
            _outlookCalendarItems.Sort("[Start]");
            _outlookCalendarItems.IncludeRecurrences = true;

            string minTime = GetDateInString(_minTime);
            string maxTime = GetDateInString(_maxTime);
            var filterString = "[Start] >= '" + minTime + "' AND [End] < '" + maxTime + "'";
            _outlookCalendarItems = _outlookCalendarItems.Restrict(filterString);
        }

        public List<AppointmentItem> GetAllItems(DateTime startDate, DateTime finishDate)
        {
            var resultList = new List<AppointmentItem>();
            _minTime = startDate.ToUniversalTime().Date;
            _maxTime = finishDate.ToUniversalTime();

            UpdateCalendarInfo();

            foreach (AppointmentItem item in _outlookCalendarItems)
            {
                if (item.Start > finishDate)
                    break;
                resultList.Add(item);
            }
            return resultList;
        }

        public void PushEvents(List<AppointmentItem> items)
        {
            UpdateCalendarInfo();
            foreach (var eventToPush in items)
            {
                eventToPush.Save();
            }
        }

        public void DeleteEvents(List<SynchronEvent> events)
        {
            UpdateCalendarInfo();

            foreach (AppointmentItem item in _outlookCalendarItems)
            {
                if (item.Start > _maxTime)
                    break;
                if (string.IsNullOrEmpty(item.Mileage))
                    continue;
                foreach (var eventToDelete in events)
                {
                    if (item.Mileage == eventToDelete.GetId())
                        item.Delete();
                }
            }
        }

        public void UpdateEvents(List<SynchronEvent> events)
        {
            UpdateCalendarInfo();

            foreach (AppointmentItem item in _outlookCalendarItems)
            {
                if (item.Start > _maxTime)
                    break;
                if (string.IsNullOrEmpty(item.Mileage))
                    continue;
                foreach (var eventToUpdate in events)
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

        private string GetDateInString(DateTime curDate)
        {
            string result = "";
            result += curDate.Day.ToString() + "/" + curDate.Month.ToString() + "/" + curDate.Year.ToString();
            result += " " + curDate.Hour.ToString() + ":" + curDate.Minute.ToString();
            return result;
        }
    }
}
