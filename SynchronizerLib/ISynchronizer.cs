using System;
using System.Collections.Generic;
using SynchronizerLib.CalendarServices;

namespace SynchronizerLib
{
    public interface ISynchronizer
    {
        void Synchronize(CalendarStore calendarStore, DateTime startDate, DateTime finishDate);
    }
}
