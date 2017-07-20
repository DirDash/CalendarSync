using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace SynchronizerLib
{
    public class EventTransformer
    {
        public List<SynchronEvent> Transform(IEnumerable<SynchronEvent> events, IEnumerable<EventTransformation> transformations)
        {
            var result = events.ToList();
            foreach (var transformation in transformations)
            {
                if (!String.IsNullOrEmpty(transformation.Transformation))
                {
                    string condition = "GetSource() == GetPlacement()";
                    if (!String.IsNullOrEmpty(transformation.Condition))
                        condition += " || " + transformation.Condition;
                    result = (result.AsQueryable().Where(condition).Select(transformation.Transformation) as IQueryable<SynchronEvent>).ToList();
                }
            }
            foreach (var e in events)
                if (e.GetSource() != e.GetPlacement())
                    result.Add(e);
            return result;
        }
    }
}
