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
                    if (transformation.Condition != String.Empty)
                    {
                        var notToTransformEvents = result.AsQueryable().Where("!(" + transformation.Condition + ")").ToList();                        
                        result = (result.AsQueryable().Where(transformation.Condition).Select(transformation.Transformation) as IQueryable<SynchronEvent>).ToList();
                        foreach (var ev in notToTransformEvents)
                            result.Add(ev);
                    }
                    else
                        result = (result.AsQueryable().Select(transformation.Transformation) as IQueryable<SynchronEvent>).ToList();
                }
            }
            return result;
        }
    }
}
