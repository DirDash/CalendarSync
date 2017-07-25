using System;
using System.Collections.Generic;
using SynchronizerLib.CalendarServices;

namespace SynchronizerLib
{
    public interface ISynchronizer
    {
        void SynchronizeAll(CalendarStore calendarStore, DateTime startDate, DateTime finishDate);
    }
}
