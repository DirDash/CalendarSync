﻿using System;
using NLog;
using SynchronizerLib;

namespace LoggerNamespace
{
    public class NLogLogger : ISyncLogger
    {
        private Logger _logger;
        private string _source;

        public NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _source = String.Empty;
        }

        public void Trace(string message)
        {
            _logger.Trace(_source + ": " + message);
        }

        public void Debug(string message)
        {
            _logger.Debug(_source + ": " + message);
        }

        public void Info(string message)
        {
            _logger.Info(_source + ": " + message);
        }

        public void Warn(string message)
        {
            _logger.Warn(_source + ": " + message);
        }

        public void Error(string message)
        {
            _logger.Error(_source + ": " + message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(_source + ": " + message);
        }

        public void SetSource(string newSource)
        {
            _source = newSource;
        }

        public string GetSource()
        {
            return _source;
        }
    }
}
