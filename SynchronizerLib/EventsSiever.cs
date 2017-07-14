using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace SynchronizerLib
{
    public class EventsSiever
    {
        public List<SynchronEvent> SieveEventsOnPeriodOfTime(DateTime startDate, DateTime finishDate,
            List<SynchronEvent> events)
        {
            var result = new List<SynchronEvent>();
            var currentStart = startDate.ToUniversalTime();
            var currentFinish = finishDate.ToUniversalTime();
            foreach (var currentEvent in events)
            {                
                if(currentEvent.GetAllDay())
                {
                    currentStart = currentStart.AddHours(-currentStart.Hour);
                    currentStart = currentStart.AddMinutes(-currentStart.Minute);
                    currentStart = currentStart.AddSeconds(-currentStart.Second);
                    currentStart = currentStart.AddMilliseconds(-currentStart.Millisecond-1);
                }
                if(currentStart <= currentEvent.GetStartUTC() && currentEvent.GetStartUTC() <= currentFinish)
                    result.Add(currentEvent);
            }
            return result;
        }

        public List<SynchronEvent> Sieve(List<SynchronEvent> events, List<string> filtres)
        {                      
            foreach (var filter in filtres)
                events = events.AsQueryable().Where("GetPlacement() != GetSource() || " + filter).ToList();
            return events;
        }
    }
}
