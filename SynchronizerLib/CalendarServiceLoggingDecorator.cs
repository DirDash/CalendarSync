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
            _logger.Info(String.Format("Pushed {0} events.", events.Count));
        }

        public void UpdateEvents(List<SynchronEvent> needToUpdate)
        {
            _calendarService.UpdateEvents(needToUpdate);
            _logger.Info(String.Format("Updated {0} events.", needToUpdate.Count));
        }
    }
}
