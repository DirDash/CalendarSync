using System;

namespace SynchronizerLib
{
    public class SieveRule
    {
        private Func<SynchronEvent, bool> checking;

        public SieveRule(Func<SynchronEvent, bool> checking)
        {
            this.checking = checking;
        }

        public bool Check(SynchronEvent synchronEvent)
        {
            return checking(synchronEvent);
        }
    }
}
