using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizerLib
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
