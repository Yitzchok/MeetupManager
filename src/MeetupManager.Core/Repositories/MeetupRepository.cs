using System;
using System.Net;
using MeetupManager.Core.JDto;
using Newtonsoft.Json;

namespace MeetupManager.Core.Repositories
{
    public class MeetupRepository : IMeetupRepository
    {
        public virtual string ApiKey { get; set; }


        public MeetupRepository(string apiKey)
        {
            ApiKey = apiKey;
        }

        public virtual RsvpJDto GetRsvpsForEvent(long eventId)
        {
            string json = new WebClient().DownloadString(string.Format("http://api.meetup.com/rsvps.json/?event_id={0}&key={1}", eventId, ApiKey));
            if (string.IsNullOrEmpty(json))
            {
                throw new NullReferenceException();
            }

            return JsonConvert.DeserializeObject<RsvpJDto>(json);
        }
    }
}