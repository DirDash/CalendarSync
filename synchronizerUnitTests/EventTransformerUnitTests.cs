using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynchronizerLib;
using Xunit;
using Assert = Xunit.Assert;
using SynchronizerLib.Events;
using SynchronizerLib.CalendarServices;

namespace SynchronizerLibUnitTests
{
    public class EventTransformerUnitTests
    {
        [Fact]
        public void TransformationIsCorrect_FieldChanged()
        {
            var newEvent = (new SynchronEvent()).SetLocation("Place");
            string transformation = @"SetLocation(""New Place"")";
            var result = (new EventTransformer()).Transform(new SynchronEvent[] { newEvent },
                                               new EventTransformation[] { new EventTransformation("", transformation) });
            Assert.Equal(result[0].GetLocation(), "New Place");
        }

        [Fact]
        public void TransformationWithConditionIsCorrect_OnlyOneEventChanged()
        {
            var eventList = new List<SynchronEvent> { (new SynchronEvent()).SetCategory("To Transform").SetLocation("Place"),
                                                      (new SynchronEvent()).SetCategory("Not To Transform").SetLocation("Place")};
            string condition = @"GetCategory() == ""To Transform""";
            string transformation = @"SetLocation(""New Place"")";
            var result = (new EventTransformer()).Transform(eventList,
                                               new EventTransformation[] { new EventTransformation(condition, transformation) });
            int eventChangedNumber = 0;
            foreach (var e in result)
                if (e.GetLocation() == "New Place")
                    eventChangedNumber++;
            Assert.Equal(1, eventChangedNumber);
        }
    }
}
