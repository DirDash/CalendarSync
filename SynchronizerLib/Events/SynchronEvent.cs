using System;
using System.Collections.Generic;

namespace SynchronizerLib.Events
{
    public class SynchronEvent
    {
        private string _id;
        private string _subject;
        private string _source; //original calendar
        private string _placement; //current calendar
        private DateTime _startTimeUTC;
        private DateTime _finishTimeUTC;
        private int _duration;
        private string _location;
        private List<string> _companions;
        private string _description;       
        private bool _isAllDayEvent;
        private string _category;

        public SynchronEvent()
        {
            _id = String.Empty;
            _subject = String.Empty;
            _source = "unknown";
            _placement = "unknown";
            _startTimeUTC = new DateTime();
            _finishTimeUTC = new DateTime();
            _duration = 0;
            _location = String.Empty;
            _companions = new List<string>();
            _description = String.Empty;
            _isAllDayEvent = false;
            _category = String.Empty;
        }

        public string GetId()
        {
            return _id;
        }

        public string GetSubject()
        {
            return _subject;
        }

        public string GetSource()
        {
            return _source;
        }

        public string GetPlacement()
        {
            return _placement;
        }

        public DateTime GetStartUTC()
        {
            return _startTimeUTC;
        }

        public DateTime GetFinishUTC()
        {
            return _finishTimeUTC;
        }

        public int GetDuration()
        {
            return _duration;
        }

        public string GetLocation()
        {
            return _location;
        }

        public List<string> GetParticipants()
        {
            _companions.Sort();
            return _companions;
        }

        public string GetDescription()
        {
            return _description;
        }

        public bool GetIsAllDay()
        {
            return _isAllDayEvent;
        }

        public string GetCategory()
        {
            return _category;
        }

        public SynchronEvent SetId(string id)
        {
            _id = id;
            return this;
        }

        public SynchronEvent SetSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public SynchronEvent SetSource(string source)
        {
            _source = source;
            return this;
        }

        public SynchronEvent SetPlacement(string placement)
        {
            _placement = placement;
            return this;
        }

        public SynchronEvent SetStartUTC(DateTime startDateTime)
        {
            _startTimeUTC = startDateTime.ToUniversalTime();
            return this;
        }

        public SynchronEvent SetFinishUTC(DateTime finishDateTime)
        {
            _finishTimeUTC = finishDateTime.ToUniversalTime();
            return this;
        }

        public SynchronEvent SetDuration(int duration)
        {
            _duration = duration;
            return this;
        }

        public SynchronEvent SetLocation(string location)
        {
            _location = location;
            return this;
        }

        public SynchronEvent AddCompanion(string participant)
        {
            _companions.Add(participant);
            return this;
        }

        public SynchronEvent SetCompanions(string allParticipants)
        {
            _companions = ParseParticipantsString(allParticipants);
            return this;
        }

        private List<string> ParseParticipantsString(string stringOfParticipants)
        {
            if (stringOfParticipants == null)
                return new List<string>();

            var result = new List<string>(stringOfParticipants.Split(';'));
            result.RemoveAll(s => s.IndexOf("@") < 0);
            for (int i = 0; i < result.Count; ++i)
            {
                result[i] = result[i].TrimStart(new char[1] { ' ' });
                result[i] = result[i].TrimEnd(new char[1] { ' ' });
            }
            return result;
        }

        public SynchronEvent SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public SynchronEvent SetIsAllDay(bool isAllDay)
        {
            _isAllDayEvent = isAllDay;
            return this;
        }        

        public SynchronEvent SetCategory(string category)
        {
            _category = category;
            return this;
        }        

        public bool CompareOnEqual(SynchronEvent compareEvent)
        {
            bool result = true;
            result = this.GetId() == compareEvent.GetId() && this.GetLocation() == compareEvent.GetLocation() && this.GetSubject() == compareEvent.GetSubject() &&
                this.GetStartUTC() == compareEvent.GetStartUTC() && this.GetFinishUTC() == compareEvent.GetFinishUTC() && this.GetDescription() == compareEvent.GetDescription();
            result &= this.GetParticipants().Count == compareEvent.GetParticipants().Count;
            for (int i = 0; i < _companions.Count && result; ++i)
                result &= _companions[i] == compareEvent._companions[i];
            return result;
        }
    }
}
