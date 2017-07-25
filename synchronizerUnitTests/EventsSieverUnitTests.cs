using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchronizerLib;
using Xunit;
using Assert = Xunit.Assert;
using SynchronizerLib.Events;

namespace SynchronizerLibUnitTests
{
    public class EventsSieverUnitTests
    {
        [Fact]
        public void SieveByStringField_OneFromThreeRemains()
        {
            var eventList = new List<SynchronEvent> { (new SynchronEvent()).SetLocation("First"),
                                                      (new SynchronEvent()).SetLocation("Second"),
                                                      (new SynchronEvent()).SetLocation("First") };
            string sieveRule = @"GetLocation() == ""Second""";
            var newEventList = (new EventsSiever()).Sieve(eventList, new string[] { sieveRule });
            Assert.Equal(1, newEventList.Count);
        }
    }
}
