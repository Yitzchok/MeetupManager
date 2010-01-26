using System.Collections.Generic;
using MeetupManager.Core.Domain;

namespace MeetupManager.Core.Services
{
    public interface IRsvpManager
    {
        IEnumerable<Attendee> GetAttendees(IList<RsvpItem> items);
        Attendee GetAttendee(RsvpItem item, int guestNumber);
    }
}