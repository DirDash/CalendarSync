using System;
using Xunit;
using Assert = Xunit.Assert;
using SynchronizerLib;
using SynchronizerLib.CalendarServices;

namespace SynchronizerLibUnitTests
{
    public class CalendarStoreUnitTests
    {
        [Fact]
        public void CreationIsCorrect_CountsAreEqual()
        {
            var firstCalendar = new CalendarServiceStub();
            var secondCalendar = new CalendarServiceStub();
            var thirdCalendar = new CalendarServiceStub();
            var store = new CalendarStore(new ICalendarService[] { firstCalendar, secondCalendar, thirdCalendar });
            Assert.Equal(3, store.Calendars.Count);
        }
    }
}
