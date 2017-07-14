using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public interface ISynchronizer
    {
        void SynchronizeAll(CalendarStore calendarStore, DateTime startDate, DateTime finishDate);
    }
}
