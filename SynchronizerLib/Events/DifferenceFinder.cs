using System.Collections.Generic;

namespace SynchronizerLib.Events
{
    public class DifferenceFinder
    {
        private bool IsExistIn(SynchronEvent needToCheck, List<SynchronEvent> events)
        {
            foreach (var curevent in events)
                if (curevent.GetId() == needToCheck.GetId())
                    return true;
            return false;
        }

        public virtual List<SynchronEvent> GetDifferenceToPush(List<SynchronEvent> standartList, List<SynchronEvent> targetList)
        {
            var difference = new List<SynchronEvent>();
            foreach (var eventToCheck in standartList)
            {
                if (eventToCheck.GetSource() == eventToCheck.GetPlacement() && !IsExistIn(eventToCheck, targetList))
                    difference.Add(eventToCheck);
            }
            return difference;
        }

        public virtual List<SynchronEvent> GetDifferenceToDelete(List<SynchronEvent> standardList, List<SynchronEvent> targetList)
        {
            var difference = new List<SynchronEvent>();
            foreach (var eventToCheck in targetList)
            {
                if (eventToCheck.GetSource() != eventToCheck.GetPlacement() && !IsExistIn(eventToCheck, standardList))
                    difference.Add(eventToCheck);
            }
            return difference;
        }

        public virtual List<SynchronEvent> GetDifferenceToUpdate(List<SynchronEvent> standardList, List<SynchronEvent> targetList)
        {
            var difference = new List<SynchronEvent>();
            foreach (var eventInStandart in standardList)
            {
                if (eventInStandart.GetSource() != eventInStandart.GetPlacement())
                    continue;
                foreach (var eventInTarget in targetList)
                {                    
                    if (eventInStandart.GetId() == eventInTarget.GetId() && !eventInStandart.CompareOnEqual(eventInTarget))
                    {
                        difference.Add(eventInStandart);
                        break;
                    }
                }
            }
            return difference;
        }
    }
}
