using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SynchronizerLib
{
    public class GoogleService : ICalendarService
    {
        static readonly string[] _scopes = {"https://www.googleapis.com/auth/calendar" };
        static readonly string _applicationName = "Google Calendar API .NET Quickstart";

        private string _serviceName = "google";
        private CalendarService _service;
        private UserCredential _credential;        
        private string _defaultTimeZone0UTC = "UTC";
        private GoogleEventConverter _converter = new GoogleEventConverter();
        private CalendarServiceConfigManager _configManager = new CalendarServiceConfigManager("googleServiceSettings");
        
        private void InitGoogleService()
        {
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var credPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            // Create Google Calendar API service
            _service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName,
            });
        }

        public List<SynchronEvent> GetAllItems(DateTime startTime, DateTime finishTime)
        {
            InitGoogleService();

            // Define parameters of request.
            var request = _service.Events.List("primary");

            request.TimeMin = startTime.ToUniversalTime();
            request.TimeMax = finishTime.ToUniversalTime();
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 1000;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var inGoogleExist = request.Execute();
            var result = new List<SynchronEvent>();

            foreach (var curEvent in inGoogleExist.Items)
                result.Add(_converter.ConvertToSynchronEvent(curEvent));
            return result;
        }

        public IEnumerable<string> GetFilters()
        {
            return new List<string> { _configManager.OutFilter };
        }

        public IEnumerable<EventTransformation> GetOutTransformations()
        {
            var transformation = _configManager.OutTransformation;
            if (transformation.Condition != String.Empty)
                transformation.Condition += " && GetSource() == GetPlacement()";
            else
                transformation.Condition = "GetSource() == GetPlacement()";
            return new List<EventTransformation> { transformation };
        }

        public IEnumerable<EventTransformation> GetInTransformations()
        {
            return new List<EventTransformation> { _configManager.InTransformation };
        }

        public IEnumerable<string> GetBannedToSyncToServices()
        {
            return _configManager.BannedToSyncToServices;
        }

        public CalendarServiceConfigManager GetConfigManager()
        {
            return _configManager;
        }

        public string GetName()
        {
            return _serviceName;
        }

        public void PushEvents(List<SynchronEvent> events)
        {
            InitGoogleService();
            
            // Define parameters of request.
            var request = _service.Events.List("primary");
            var inGoogleExist = request.Execute();
            foreach (var currentEvent in events)
            {
                var needToPush = _converter.ConvertToGoogleEvent(currentEvent);
                _service.Events.Insert(needToPush, request.CalendarId).Execute();
            }            
        }

        public void DeleteEvents(List<SynchronEvent> events)
        {
            InitGoogleService();
            // Define parameters of request.
            var request = _service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 1000;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var inGoogleExist = request.Execute();
            foreach (var eventToCheck in inGoogleExist.Items)
            {
                var eventWasFound = false;
                if (eventToCheck.ExtendedProperties == null)
                    continue;
                foreach (var needToDelete in events)
                {
                    if (eventToCheck.ExtendedProperties.Private__["id"] == needToDelete.GetId())
                    {
                        eventWasFound = true;
                        break;
                    }
                }
                if (eventWasFound)
                    _service.Events.Delete(request.CalendarId, eventToCheck.Id).Execute();
            }
        }        

        public void UpdateEvents(List<SynchronEvent> NeedToUpdate)
        {
            InitGoogleService();
            // Define parameters of request.
            var request = _service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 1000;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            
            var inGoogleExist = request.Execute();
            foreach (var eventToCheck in inGoogleExist.Items)
            {
                if (eventToCheck.ExtendedProperties == null)
                    continue;
                foreach (var needToUpdate in NeedToUpdate)
                {
                    if (eventToCheck.ExtendedProperties.Private__["id"] == needToUpdate.GetId())
                    {
                        eventToCheck.Description = needToUpdate.GetDescription();
                        eventToCheck.Summary = needToUpdate.GetSubject();
                        EventDateTime start = new EventDateTime();                        
                        start.DateTime = needToUpdate.GetStartUTC();
                        eventToCheck.Start = start;
                        eventToCheck.Start.TimeZone = _defaultTimeZone0UTC;
                        EventDateTime end = new EventDateTime();
                        end.DateTime = needToUpdate.GetFinishUTC();
                        eventToCheck.End = end;
                        eventToCheck.End.TimeZone = _defaultTimeZone0UTC;

                        EventAttendee[] attendees = new EventAttendee[needToUpdate.GetParticipants().Count];
                        List<string> AllParticipants = needToUpdate.GetParticipants();
                        for (int i = 0; i < AllParticipants.Count; ++i)
                        {
                            var evAt = new EventAttendee
                            {
                                Email = AllParticipants[i],
                                ResponseStatus = "needsAction"
                            };
                            attendees[i] = evAt;
                        }
                        eventToCheck.Attendees = attendees;

                        eventToCheck.Location = needToUpdate.GetLocation();
                        _service.Events.Update(eventToCheck, "primary", eventToCheck.Id).Execute();
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Google Calendar Service";
        }
    }
}
