using System;

namespace SynchronizerLib.SynchronEvents
{
    public class EventTransformation
    {
        public string Condition;
        public string Transformation;

        public EventTransformation(string condition, string transformation)
        {
            Condition = condition;
            Transformation = transformation;
        }
    }
}
