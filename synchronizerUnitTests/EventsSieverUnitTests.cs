using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchronizerLib;
using Xunit;
using Assert = Xunit.Assert;

namespace SynchronizerLibUnitTests
{
    public class EventsSieverUnitTests
    {
        /*
        [Fact]
        public void BeginingEvent_NonExistInResultList()
        {
            var cur = new SynchronEvent();
            cur.SetStartUTC(DateTime.Now.AddHours(-1));
            var list = new List<SynchronEvent>{cur};
            list = new EventsSiever().Sieve(list, new string[] { String.Format("GetStartUTC() >= DateTime.Parse({0})", DateTime.Now.ToUniversalTime().ToString())});
            //list = new EventsSiever().SieveEventsOnPeriodOfTime(DateTime.Now, DateTime.Now.AddHours(1), list);
            Assert.Equal(0, list.Count);
        }
        
        [Fact]
        public void EventWithStartOneHourLater_ExistInResultList()
        {
            var cur = new SynchronEvent();
            var date = DateTime.Now.ToUniversalTime();
            cur.SetStartUTC(date.AddHours(1));
            var list = new List<SynchronEvent> { cur };
            list = new EventsSiever().SieveEventsOnPeriodOfTime(date, date.AddHours(2), list);
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void EventWithStartNow_ExistInResult()
        {
            var cur = new SynchronEvent();
            var start = DateTime.Now.ToUniversalTime();
            cur.SetStartUTC(start);
            var list = new List<SynchronEvent> { cur };
            list = new EventsSiever().SieveEventsOnPeriodOfTime(start, start.AddMinutes(12), list);
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void OneGoogAndOneBadEvents_ResultContainsOnlyOne()
        {
            var date = DateTime.Now.ToUniversalTime();
            var cur1 = new SynchronEvent();
            cur1.SetStartUTC(date.AddHours(-1));

            var cur2 = new SynchronEvent();
            cur2.SetStartUTC(date.AddMinutes(5));

            var list = new List<SynchronEvent> { cur1, cur2 };
            list = new EventsSiever().SieveEventsOnPeriodOfTime(date, date.AddHours(1), list);
            Assert.Equal(1, list.Count);
        }
        */
    }
}
