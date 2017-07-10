﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizerLib
{
    public interface ISyncLogger : ICloneable
    {
        void Trace(string message);

        void Debug(string message);

        void Info(string message);

        void Warn(string message);

        void Error(string message);

        void Fatal(string message);

        void SetSource(string newSource);

        string GetSource();
    }
}