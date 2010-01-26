using System.Collections.Generic;
using MeetupManager.Core.Domain;

namespace MeetupManager.Core.Services
{
    public interface IMeetupService
    {
        IList<RsvpItem> GetRsvpsForEvent(long eventId);
    }
}