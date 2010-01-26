using System;
using System.Collections.Generic;

namespace MeetupManager.Core.Domain
{
    public class RsvpItem
    {
        public IList<string> Answers { get; set; }
        public string City { get; set; }
        public string Comment { get; set; }
        public decimal Coord { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public int Guests { get; set; }
        public string Link { get; set; }
        public decimal Lon { get; set; }
        public string Name { get; set; }
        public RsvpReponseType Response { get; set; }
        public string State { get; set; }
        public DateTime Updated { get; set; }
        public string Zip { get; set; }
        //private IList<Guest> attendees;

        //public IList<Guest> Attendees()
        //{
        //    if (attendees == null)
        //    {
        //        attendees = new List<Guest>();

        //    }
        //    return attendees;
        //}
    }

    public class Guest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}