using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public class SynchronizerLoggingDecorator : Synchronizer
    {
        private Synchronizer _synchronizer;
        private ISyncLogger _logger;        

        public SynchronizerLoggingDecorator(Synchronizer synchronizer, ISyncLogger logger) : base ()
        {
            _synchronizer = synchronizer;
            _logger = logger;
            _logger.SetSource("Synchronizer");
            for (int i = 0; i < _synchronizer.Calendars.Count; i++)
            {
                _synchronizer.Calendars[i] = new CalendarServiceLoggingDecorator(_synchronizer.Calendars[i], (ISyncLogger)_logger.Clone());
            }           
        }

        public override void Synchronize(DateTime startDate, DateTime finishDate)
        {
            _logger.Info("Synchronization started.");
            try
            {
                _synchronizer.Synchronize(startDate, finishDate);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.ToString());
                throw exception;
            }
            _logger.Info("Synchonization finished.");
        }
    }
}
