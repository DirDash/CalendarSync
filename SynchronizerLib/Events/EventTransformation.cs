using System;

namespace SynchronizerLib.Events
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
