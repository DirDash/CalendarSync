using System;
using System.Collections.Generic;

namespace SynchronizerLib
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
                logMessage += String.Format("{0} {1} {2} {3} - {4}", events[i].GetSubject(), events[i].GetLocation(), events[i].GetStart().ToShortDateString(),
                                                          events[i].GetStart().ToShortTimeString(), events[i].GetFinish().ToShortTimeString());
                if (i < events.Count - 1)
                    logMessage += Environment.NewLine;
                else
                    logMessage += ";";
            }
            return logMessage;
        }
    }
}
