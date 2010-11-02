using System.Collections.Generic;
using MeetupManager.Core.Domain;
using MeetupManager.Core.Repositories;
using MeetupManager.Data.Repositories;
using NHibernate.Criterion;
using System.Linq;

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
            var member = _memberRepository.FindAll(
                new Dictionary<string, object>() { { "MemberName", name } }
                ).First();

            if (member == null)
                return null;

            member.FirstName = firstName;
            member.LastName = lastName;

            return _memberRepository.SaveOrUpdate(member);
        }
    }
}