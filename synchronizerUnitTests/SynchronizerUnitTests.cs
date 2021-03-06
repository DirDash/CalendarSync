﻿using SynchronizerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Assert = Xunit.Assert;
using SynchronizerLib.SynchronEvents;
using SynchronizerLib.CalendarServices;

namespace SynchronizerLibUnitTests
{
    public class SynchronizerUnitTests
    {
        [Fact]
        public void CheckOnNonModyfing_NonOfThem()
        {           
            var calendarA = new CalendarServiceStub();
            var calendarB = new CalendarServiceStub();
            var calendarStore = new CalendarStore( new List<ICalendarService>() { calendarA, calendarB });
            var synchronizer = new Synchronizer();
            DateTime startData = DateTime.Now.ToUniversalTime();
            DateTime finishDate = startData.AddMonths(1);
            synchronizer.Synchronize(calendarStore, startData, finishDate);

            Assert.True(calendarA.GetAllItems(startData, finishDate).Count == calendarB.GetAllItems(startData, finishDate).Count
                && calendarB.GetAllItems(startData, finishDate).Count == 0);
        }
        
        [Fact]
        public void TwoCalendarsOneNonEmpty_AddedToNext1()
        {            
            var calendarA = new CalendarServiceStub();
            var calendarB = new CalendarServiceStub();
            var calendarStore = new CalendarStore(new List<ICalendarService>() { calendarA, calendarB });
            var synchronizer = new Synchronizer();
            DateTime startData = DateTime.Now.ToUniversalTime();
            calendarA.AddEvent(new SynchronEvent().SetId("1234").SetStartUTC(startData).SetFinishUTC(startData.AddDays(1))
                .SetPlacement("1").SetSource("1"));
            DateTime finishDate = startData.AddMonths(1);
            synchronizer.Synchronize(calendarStore, startData, finishDate);

            Assert.True(calendarA.GetAllItems(startData, finishDate).Count == calendarB.GetAllItems(startData, finishDate).Count
                && calendarB.GetAllItems(startData, finishDate).Count == 1);
        }

        [Fact]
        public void NeedToDelete_Deleted()
        {            
            var calendarA = new CalendarServiceStub();
            var calendarB = new CalendarServiceStub();
            var calendarStore = new CalendarStore(new List<ICalendarService>() { calendarA, calendarB });
            var synchronizer = new Synchronizer();
            DateTime startData = DateTime.Now.ToUniversalTime();
            calendarA.AddEvent(new SynchronEvent().SetId("1234").SetStartUTC(startData).SetFinishUTC(startData.AddDays(1))
                .SetPlacement("1").SetSource("2"));
            DateTime finishDate = startData.AddMonths(1);
            synchronizer.Synchronize(calendarStore, startData, finishDate);

            Assert.True(calendarA.GetAllItems(startData, finishDate).Count == calendarB.GetAllItems(startData, finishDate).Count
                && calendarB.GetAllItems(startData, finishDate).Count == 0);
        }

        [Fact]
        public void NeedToUpdate_Updated()
        {           
            var calendarA = new CalendarServiceStub();
            var calendarB = new CalendarServiceStub();
            var calendarStore = new CalendarStore(new List<ICalendarService>() { calendarA, calendarB });
            var synchronizer = new Synchronizer();
            DateTime startData = DateTime.Now;
            DateTime finishDate = startData.AddMonths(1);
            var curEvent = new SynchronEvent().SetId("1234").SetStartUTC(startData.AddMinutes(15)).SetFinishUTC(finishDate)
                .SetPlacement("1").SetSource("2");

            calendarA.AddEvent(curEvent);
            calendarB.AddEvent(curEvent.SetPlacement("2").SetSubject("check"));
            
            synchronizer.Synchronize(calendarStore, startData, finishDate);

            Assert.True(calendarA.GetAllItems(startData, finishDate)[0].GetSubject() == "check");
        }

        [Fact]
        public void NeedToUpdateTime_Updated()
        {            
            var calendarA = new CalendarServiceStub();
            var calendarB = new CalendarServiceStub();
            var calendarStore = new CalendarStore(new List<ICalendarService>() { calendarA, calendarB });
            var synchronizer = new Synchronizer();
            DateTime startData = DateTime.Now.ToUniversalTime();
            DateTime finishDate = startData.AddMonths(1);
            var curEvent = new SynchronEvent().SetId("1234").SetStartUTC(startData.AddMinutes(15)).SetFinishUTC(finishDate)
                .SetPlacement("1").SetSource("2");

            calendarA.AddEvent(curEvent);
            calendarB.AddEvent(curEvent.SetPlacement("2").SetStartUTC(startData.AddMinutes(30)));

            synchronizer.Synchronize(calendarStore, startData, finishDate);

            Assert.True(calendarA.GetAllItems(startData, finishDate)[0].GetStartUTC() == startData.AddMinutes(30));
        }
    }

    public class CalendarServiceStub : ICalendarService
    {
        public List<SynchronEvent> Events { get; private set; }
        private string _id;

        private bool SameId(SynchronEvent cur)
        {
            return cur.GetId() == _id;
        }

        public void AddEvent(SynchronEvent toAdd)
        {
            Events.Add(toAdd);
        }

        public CalendarServiceStub()
        {
            Events = new List<SynchronEvent>();
        }

        public string ServiceName
        {
            get { return "Stub"; }
        }

        public CalendarServiceConfigManager ConfigManager
        {
            get { return new CalendarServiceConfigManager("testSection"); }
        }

        public void DeleteEvents(List<SynchronEvent> events)
        {
            foreach(var curEvent in events)
            {
                _id = curEvent.GetId();
                Events.RemoveAll(SameId);
            }  
        }

        public List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime)
        {
            return Events;
        }

        public void PushEvents(List<SynchronEvent> events)
        {
            foreach (var curEvent in events)
                Events.Add(curEvent);
        }

        public void UpdateEvents(List<SynchronEvent> needToUpdate)
        {
            foreach (var curEvent in needToUpdate)
            {
                _id = curEvent.GetId();
                for (int i = 0; i < Events.Count; ++i)
                    if (SameId(Events[i]))
                        Events[i] = curEvent;
            }
        }
    }    
}
