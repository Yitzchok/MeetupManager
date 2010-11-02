using MeetupManager.Core.Domain;

namespace MeetupManager.Data
{
    public interface IMemberService
    {
        Member UpdateByMemberName(string name, string firstName, string lastName);
    }
}