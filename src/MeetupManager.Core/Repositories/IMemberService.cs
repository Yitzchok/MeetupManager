using MeetupManager.Core.Domain;

namespace MeetupManager.Core.Repositories
{
    public interface IMemberService
    {
        Member UpdateByMemberName(string name, string firstName, string lastName);
    }
}