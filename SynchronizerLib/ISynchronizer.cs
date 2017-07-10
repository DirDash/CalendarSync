using System;

namespace SynchronizerLib
{
    public interface ISynchronizer
    {
        void Synchronize(DateTime startDate, DateTime finishDate);
    }
}
