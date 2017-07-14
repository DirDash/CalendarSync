using System;
using System.Collections.Generic;

namespace SynchronizerLib
{
    public interface ISynchronizer
    {
        void SynchronizeAll(IEnumerable<ICalendarService> calendars, DateTime startDate, DateTime finishDate);
    }
}
