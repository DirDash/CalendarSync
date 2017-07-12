using System;
using System.Collections.Generic;
using Google.Apis.Calendar.v3.Data;

namespace SynchronizerLib
{
    public class GoogleEventConverter
    {
        public SynchronEvent ConvertToSynchronEvent(Event googleEvent)
        {
            var result = new SynchronEvent();
            if (googleEvent.Attendees != null)
            {
                foreach (var email in googleEvent.Attendees)
                    result.AddCompanions(email.Email);
            }
            if (googleEvent.Start.Date != null)
            {
                result.SetAllDay(true);
                string date = googleEvent.Start.Date;
                string[] q = date.Split('-');
                var year = int.Parse(q[0]);
                var month = int.Parse(q[1]);
                var day = int.Parse(q[2]);
                DateTime buf = new DateTime(year, month, day);
                result.SetStartUTC(buf);
                result.SetFinishUTC(buf.AddDays(1));
            }
            else
            {
                result.SetStartUTC(googleEvent.Start.DateTime.Value)
                .SetFinishUTC(googleEvent.End.DateTime.Value);
            }
            result
            .SetLocation(googleEvent.Location)
            .SetDescription(googleEvent.Description)
            .SetSubject(googleEvent.Summary)
            .SetSource(CalendarServiceEnum.Google.ToString())
            .SetId(googleEvent.Id)
            .SetPlacement(CalendarServiceEnum.Google.ToString());

            if (googleEvent.ExtendedProperties != null && googleEvent.ExtendedProperties.Private__!= null
                && googleEvent.ExtendedProperties.Private__["source"] != CalendarServiceEnum.Google.ToString())
            {
                result.SetSource(googleEvent.ExtendedProperties.Private__["source"]);
                result.SetId(googleEvent.ExtendedProperties.Private__["id"]);
            }
            else
            {
                result.SetSource(CalendarServiceEnum.Google.ToString());
            }
            return result;
        }

        public Event ConvertToGoogleEvent(SynchronEvent synchronEvent)
        {
            // todo: timezones
            // Google API: the default is the time zone of the calendar.
            var eventDateTime = new EventDateTime
            {
                DateTime = synchronEvent.GetStartUTC(),
                //TimeZone = "Europe/Moscow"
            };
            var eventDateTimeEnd = new EventDateTime
            {
                DateTime = synchronEvent.GetFinishUTC(),
                //TimeZone = "Europe/Moscow"
            };
            var googleEvent = new Event
            {
                Location = synchronEvent.GetLocation(),
                Summary = synchronEvent.GetSubject(),
                Start = eventDateTime,
                End = eventDateTimeEnd,
                Description = synchronEvent.GetDescription(),
            };

            if (synchronEvent.GetAllDay())
            {
                googleEvent.Start.DateTime = null;
                googleEvent.Start.DateTimeRaw = null;
                googleEvent.Start.Date = synchronEvent.GetStartUTC().Year.ToString() + "-" + "0" + synchronEvent.GetStartUTC().Month.ToString() + "-" + synchronEvent.GetStartUTC().Day.ToString();
                googleEvent.End.DateTime = null;
                googleEvent.End.DateTimeRaw = null;
                googleEvent.End.Date = synchronEvent.GetStartUTC().Year.ToString() + "-" + "0" + synchronEvent.GetStartUTC().Month.ToString() + "-" + synchronEvent.GetStartUTC().AddDays(1).Day.ToString();
            }
            if (synchronEvent.GetSource() != CalendarServiceEnum.Google.ToString())
            {
                googleEvent.ExtendedProperties = new Event.ExtendedPropertiesData
                {
                    Private__ = new Dictionary<string, string> { { "source", synchronEvent.GetSource() }, { "id", synchronEvent.GetId() } }
                };
            }

            EventAttendee[] attendees = new EventAttendee[synchronEvent.GetParticipants().Count];
            List<string> allParticipants = synchronEvent.GetParticipants();
            for (int i = 0; i < allParticipants.Count; ++i)
            {
                var evAt = new EventAttendee
                {
                    Email = allParticipants[i],
                    ResponseStatus = "needsAction"
                };
                attendees[i] = evAt;
            }
            googleEvent.Attendees = attendees;
            return googleEvent;
        }
    }
}
