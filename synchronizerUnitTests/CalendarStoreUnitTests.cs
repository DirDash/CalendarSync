using System;
using Xunit;
using Assert = Xunit.Assert;
using SynchronizerLib;

namespace SynchronizerLibUnitTests
{
    public class CalendarStoreUnitTests
    {
        [Fact]
        public void AdditionIsCorrect_CountsAreEqual()
        {
            var firstCalendar = new CalendarServiceStub();
            var secondCalendar = new CalendarServiceStub();
            var thirdCalendar = new CalendarServiceStub();
            var store = new CalendarStore();
            store.AddCalendar(firstCalendar);
            store.AddCalendar(secondCalendar);
            store.AddCalendar(thirdCalendar);
            Assert.Equal(3, store.GetCalendars().Count);
        }

        [Fact]
        public void RemovalIsCorrect_CountsAreEqual()
        {
            var firstCalendar = new CalendarServiceStub();
            var secondCalendar = new CalendarServiceStub();
            var thirdCalendar = new CalendarServiceStub();
            var store = new CalendarStore();
            store.AddCalendar(firstCalendar);
            store.AddCalendar(secondCalendar);
            store.AddCalendar(thirdCalendar);
            store.RemoveCalendar(secondCalendar);
            Assert.Equal(2, store.GetCalendars().Count);
        }

        [Fact]
        public void ChangeSyncModeIsCorrect_ModeChanged()
        {
            var firstCalendar = new CalendarServiceStub();
            var secondCalendar = new CalendarServiceStub();
            var thirdCalendar = new CalendarServiceStub();
            var store = new CalendarStore();
            store.AddCalendar(firstCalendar);
            store.AddCalendar(secondCalendar);
            store.AddCalendar(thirdCalendar);
            store.ChangeSyncMode(firstCalendar, thirdCalendar, false);
            Assert.Equal(false, store.SyncIsAllowed(firstCalendar, thirdCalendar));
        }
    }
}
