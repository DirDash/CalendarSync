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
                _logger.Error(exception.Message);
                throw exception;
            }
            _logger.Info("Synchonization finished.");
        }
    }
}
