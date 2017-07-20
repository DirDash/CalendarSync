using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace SynchronizerLib
{
    public class EventsSiever
    {
        public List<SynchronEvent> Sieve(List<SynchronEvent> events, IEnumerable<string> filtres)
        {
            foreach (var filter in filtres)
            {
                if (filter != String.Empty)
                    events = events.AsQueryable().Where("GetPlacement() != GetSource() || " + filter).ToList();
            }
            return events;
        }
    }
}
