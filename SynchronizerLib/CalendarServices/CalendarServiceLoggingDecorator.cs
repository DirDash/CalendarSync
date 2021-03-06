﻿using System;
using System.Collections.Generic;
using SynchronizerLib.SynchronEvents;

namespace SynchronizerLib.CalendarServices
{
    public class CalendarServiceLoggingDecorator : ICalendarService
    {
        private ICalendarService _calendarService;
        private ISyncLogger _logger;

        public CalendarServiceLoggingDecorator(ICalendarService calendarService, ISyncLogger logger)
        {
            _calendarService = calendarService;
            _logger = logger;
            _logger.SetSource(_calendarService.ToString());
        }

        public string ServiceName
        {
            get { return _calendarService.ServiceName; }
        }

        public CalendarServiceConfigManager ConfigManager
        {
            get { return _calendarService.ConfigManager; }
        }

        public List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime)
        {
            return _calendarService.GetAllItems(startTime, finishTime);
        }

        public void PushEvents(List<SynchronEvent> events)
        {
            _calendarService.PushEvents(events);
            _logger.Info("Pushed " + FormatEvents(events));
        }

        public void DeleteEvents(List<SynchronEvent> events)
        {
            _calendarService.DeleteEvents(events);             
            _logger.Info("Deleted " + FormatEvents(events));
        }

        public void UpdateEvents(List<SynchronEvent> needToUpdate)
        {
            _calendarService.UpdateEvents(needToUpdate);
            _logger.Info("Updated " + FormatEvents(needToUpdate));
        }

        private string FormatEvents(List<SynchronEvent> events)
        {
            string logMessage = String.Format("{0} events", events.Count);
            if (events.Count != 0)
                logMessage += ":" + Environment.NewLine;
            else
                logMessage += ".";
            for (int i = 0; i < events.Count; i++)
            {
                logMessage += String.Format("{0} {1} {2} {3} - {4} ({5} {6})", events[i].GetSubject(), events[i].GetLocation(), events[i].GetStartUTC().ToLocalTime().ToShortDateString(),
                                                          events[i].GetStartUTC().ToLocalTime().ToShortTimeString(), events[i].GetFinishUTC().ToLocalTime().ToShortTimeString(), events[i].GetSource(), events[i].GetId());
                if (i < events.Count - 1)
                    logMessage += Environment.NewLine;
                else
                    logMessage += ";";
            }
            return logMessage;
        }

        public override string ToString()
        {
            return _calendarService.ToString();
        }
    }
}
