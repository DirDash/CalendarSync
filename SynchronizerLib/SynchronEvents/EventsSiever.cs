using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace SynchronizerLib.SynchronEvents
{
    public class EventsSiever
    {
        public List<SynchronEvent> Sieve(List<SynchronEvent> events, string filter)
        {
            if (filter != String.Empty)
                events = events.AsQueryable().Where(filter).ToList();
            return events;
        }
    }
}
