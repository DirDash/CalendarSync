using System;

namespace SynchronizerLib
{
    public class SynchronizerLoggingDecorator : ISynchronizer
    {
        private Synchronizer _synchronizer;
        private ISyncLogger _logger;        

        public SynchronizerLoggingDecorator(Synchronizer synchronizer, ISyncLogger logger)
        {
            _synchronizer = synchronizer;
            _logger = logger;
            _logger.SetSource("Synchronizer");
        }

        public void Synchronize(DateTime startDate, DateTime finishDate)
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
            _logger.Info("Synchonization successfully finished.");
        }
    }
}
