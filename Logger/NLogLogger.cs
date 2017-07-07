using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SynchronizerLib;

namespace LoggerNamespace
{
    public class NLogLogger : ISyncLogger
    {
        private Logger _logger;
        private string _associatedClassName;

        public NLogLogger(string currentClassName)
        {
            _associatedClassName = currentClassName;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }
    }
}
