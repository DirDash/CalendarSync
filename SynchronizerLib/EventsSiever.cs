using System;
using System.Collections.Generic;

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

        public List<SynchronEvent> Sieve(List<SynchronEvent> events, List<SieveRule> rules)
        {
            var result = new List<SynchronEvent>();
            foreach (var eventToCheck in events)
            {
                bool eventIsSuit = true;
                if (rules != null && eventToCheck.GetPlacement() == eventToCheck.GetSource())
                    foreach (var rule in rules)
                    {
                        if (!rule.Check(eventToCheck))
                        {
                            eventIsSuit = false;
                            break;
                        }
                    }
                if (eventIsSuit)
                    result.Add(eventToCheck);
            }
            return result;
        }
    }
}
