using MeetupManager.Core.Domain;
using MeetupManager.Core.Repositories;
using NHibernate.Criterion;

namespace MeetupManager.Data
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public Member UpdateByMemberName(string name, string firstName, string lastName)
        {

            //this is in the wrong place - should be in the repository
            Member member = _memberRepository.FindOne(
                DetachedCriteria.For(typeof(Member))
                    .Add(Restrictions.Eq("MemberName", name))
                );

            if (member == null)
                return null;

            member.FirstName = firstName;
            member.LastName = lastName;

            return _memberRepository.SaveOrUpdate(member);
        }
    }
}