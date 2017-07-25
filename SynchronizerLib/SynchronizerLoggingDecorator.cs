using System;
using SynchronizerLib.CalendarServices;

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

        public void SynchronizeAll(CalendarStore calendarStore, DateTime startDate, DateTime finishDate)
        {
            _logger.Info("Synchronization started.");
            try
            {
                _synchronizer.SynchronizeAll(calendarStore, startDate, finishDate);
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
