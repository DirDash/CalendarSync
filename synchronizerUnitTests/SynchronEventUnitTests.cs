using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;
using SynchronizerLib;

namespace SynchronizerLibUnitTests
{
    public class SynchronEventUnitTests
    {
        [Fact]
        public void SetSomeId_ReturnTheSame()
        {
            var currentEvent = new SynchronEvent();
            string id = "1231412";
            currentEvent.SetId(id);
            Assert.Equal(id, currentEvent.GetId());
        }

        [Fact]
        public void NoSetId_ReturnEmptyString()
        {
            var currentEvent = new SynchronEvent();
            Assert.Equal("", currentEvent.GetId());
        }

        [Fact]
        public void SameListCompanionsButAddedInTheDifferentWay_returnSame()
        {
            var current1 = new SynchronEvent().AddCompanion("1@ya.ru").AddCompanion("2@ya.ru");
            var current2 = new SynchronEvent().AddCompanion("2@ya.ru").AddCompanion("1@ya.ru");
            Assert.True(current1.CompareOnEqual(current2));
        }

        [Fact]
        public void DifferentListCompanions_returnDifferent()
        {
            var current1 = new SynchronEvent().AddCompanion("1@ya.ru").AddCompanion("2@ya.ru");
            var current2 = new SynchronEvent().AddCompanion("2@ya.ru").AddCompanion("0@ya.ru");
            Assert.False(current1.CompareOnEqual(current2));
        }

        [Fact]
        public void BadString_ParseGood()
        {
            var current = new SynchronEvent().SetCompanions("asd asd asd ;    1@ya.ru    ");
            Assert.Equal(1, current.GetParticipants().Count);
        }

        [Fact]
        public void CombinedString_ParseGood()
        {
            var current = new SynchronEvent().SetCompanions("asd asd asd ;    1@ya.ru    ");
            Assert.Equal("1@ya.ru", current.GetParticipants()[0]);
        }
    }
}
