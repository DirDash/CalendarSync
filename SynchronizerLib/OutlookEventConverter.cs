﻿using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;

namespace SynchronizerLib
{
    public class OutlookEventConverter
    {
        public SynchronEvent ConvertToSynchronEvent(AppointmentItem outlookItem)
        {
            var result = new SynchronEvent()
                .SetStart(outlookItem.Start)
                .SetDuration(outlookItem.Duration)
                .SetLocation(outlookItem.Location)
                .SetSubject(outlookItem.Subject)
                .SetCompanions(outlookItem.RequiredAttendees)
                .SetFinish(outlookItem.End)
                .SetSource(CalendarServiceEnum.Outlook.ToString())
                .SetDescription(outlookItem.Body)
                .SetPlacement(CalendarServiceEnum.Outlook.ToString())
                .SetAllDay(outlookItem.AllDayEvent);

            if (!string.IsNullOrEmpty(outlookItem.Mileage))
            {
                //problem place
                result.SetId(outlookItem.Mileage);
                if (outlookItem.Mileage.IndexOf(CalendarServiceEnum.Outlook.ToString()) > -1)
                    result.SetSource(CalendarServiceEnum.Outlook.ToString());
                else
                {
                    result.SetSource(outlookItem.Mileage);
                    outlookItem.Save();
                }
            }
            else
            {
                // problem place
                Guid id = Guid.NewGuid();
                outlookItem.Mileage = CalendarServiceEnum.Outlook.ToString() + id.ToString();
                outlookItem.Save();
                result.SetSource(CalendarServiceEnum.Outlook.ToString());
                result.SetId(outlookItem.Mileage);
            }
            return result;
        }

        public AppointmentItem ConvertToOutlookEvent(SynchronEvent synchronEvent)
        {
            var outlookApp = new Application();

            var result = (AppointmentItem)outlookApp.CreateItem(OlItemType.olAppointmentItem);

            result.Start = synchronEvent.GetStart();
            result.End = synchronEvent.GetFinish();
            result.Subject = synchronEvent.GetSubject();
            result.Location = synchronEvent.GetLocation();
            result.Body = synchronEvent.GetDescription();
            result.AllDayEvent = synchronEvent.GetAllDay();

            string buf = String.Empty;
            List<string> AllParticipants = synchronEvent.GetParticipants();

            for (int i = 0; i < AllParticipants.Count; ++i)
            {
                if (i + 1 < AllParticipants.Count)
                    buf = buf + AllParticipants[i] + "; ";
                else
                    buf = buf + AllParticipants[i];
            }
            result.RequiredAttendees = buf;
            result.ResponseRequested = true;
            if (synchronEvent.GetSource() != CalendarServiceEnum.Outlook.ToString())
            {
                result.Mileage = synchronEvent.GetId();
            }
            return result;
        }
    }
}