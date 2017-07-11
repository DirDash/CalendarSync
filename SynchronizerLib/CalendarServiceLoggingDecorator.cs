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
            string logMessage = String.Format("Deleted {0} events", events.Count);
            if (events.Count != 0)
                logMessage += ":" + Environment.NewLine;
            else
                logMessage += ".";
            for (int i = 0; i < events.Count; i++)
            {
                logMessage += FormatEvent(events[i]);
                if (i < events.Count - 1)
                    logMessage += Environment.NewLine;
                else
                    logMessage += ";";
            }
            _logger.Info(logMessage);
        }

        public List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime)
        {
            return _calendarService.GetAllItems(startTime, finishTime);
        }

        public void PushEvents(List<SynchronEvent> events)
        {
            _calendarService.PushEvents(events);
            string logMessage = String.Format("Pushed {0} events", events.Count);
            if (events.Count != 0)
                logMessage += ":" + Environment.NewLine;
            else
                logMessage += ".";
            for (int i = 0; i < events.Count; i++)
            {
                logMessage += FormatEvent(events[i]);
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
            string logMessage = String.Format("Updated {0} events", needToUpdate.Count);
            if (needToUpdate.Count != 0)
                logMessage += ":" + Environment.NewLine;
            else
                logMessage += ".";
            for (int i = 0; i < needToUpdate.Count; i++)
            {
                logMessage += FormatEvent(needToUpdate[i]);
                if (i < needToUpdate.Count - 1)
                    logMessage += Environment.NewLine;
                else
                    logMessage += ";";
            }
            _logger.Info(logMessage);
        }

        private string FormatEvent(SynchronEvent syncEvent)
        {
            return String.Format("{0} {1} {2} {3} - {4}", syncEvent.GetSubject(), syncEvent.GetLocation(), syncEvent.GetStart().ToShortDateString(),
                                                          syncEvent.GetStart().ToShortTimeString(), syncEvent.GetFinish().ToShortTimeString());
        }
    }
}
