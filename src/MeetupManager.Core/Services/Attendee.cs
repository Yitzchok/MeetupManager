namespace MeetupManager.Core.Services
{
    public class Attendee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RSVPName { get; set; }
        public string RSVPAnswer { get; set; }
        public int RSVPGuests { get; set; }
    }
}