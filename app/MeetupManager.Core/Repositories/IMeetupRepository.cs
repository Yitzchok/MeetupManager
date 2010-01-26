using System.Collections.Generic;
using MeetupManager.Core.JDto;

namespace MeetupManager.Core.Repositories
{
    public interface IMeetupRepository
    {
        RsvpJDto GetRsvpsForEvent(long eventId);
    }
}