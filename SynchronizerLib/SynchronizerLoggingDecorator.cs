using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public class SynchronizerLoggingDecorator : Synchronizer
    {
        private Synchronizer _synchronizer;
        private ISyncLogger _logger;        

        public SynchronizerLoggingDecorator(List<ICalendarService> calendars, Synchronizer synchronizer) : base (calendars)
        {
            _synchronizer = synchronizer;
        }

        public override void Synchronize(DateTime startDate, DateTime finishDate)
        {
            //_logger.Debug("start");
            _synchronizer.Synchronize(startDate, finishDate);
            //_logger.Debug("finish");
        }
    }
}
