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

        public void DeleteEvents(List<SynchronEvent> events)
        {
            _calendarService.DeleteEvents(events);
            _logger.Info(String.Format("Deleted {0} events.", events.Count));
        }

        public List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime)
        {
            return _calendarService.GetAllItems(startTime, finishTime);
        }

        public void PushEvents(List<SynchronEvent> events)
        {
            _calendarService.PushEvents(events);
            string logMessage = String.Empty;
            logMessage += String.Format("Pushed {0} events:", events.Count) + Environment.NewLine;
            for (int i = 0; i < events.Count; i++)
            {
                logMessage += events[i].GetSubject() + " " + events[i].GetStart().ToString();
                if (i < events.Count - 1)
                    logMessage += Environment.NewLine;
                else
                    logMessage += ";";
            }
            _logger.Info(logMessage);
        }

        public void UpdateEvents(List<SynchronEvent> needToUpdate)
        {
            _calendarService.UpdateEvents(needToUpdate);
            _logger.Info(String.Format("Updated {0} events.", needToUpdate.Count));
        }
    }
}
