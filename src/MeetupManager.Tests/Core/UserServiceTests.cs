using System;
using MeetupManager.Core.Domain;
using MeetupManager.Data;
using MeetupManager.Data.Repositories;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.MeetupManager.Core
{
    public class UserServiceTests : TestFixtureBase
    {
        [Test, Ignore]
        public void Can_update_username_using_user_id()
        {
            var memberRepository = S<IMemberRepository>();
            memberRepository.Expect(x => x.SaveOrUpdate(null)).IgnoreArguments().Return(new Member
                                                                                            {
                                                                                                FirstName = "Organizer",
                                                                                                LastName = "Name"
                                                                                            });

            IMemberService memberService = new MemberService(memberRepository);
            var member = memberService.UpdateByMemberName("Organizer Name", "Organizer", "Name");

            Assert.That(member.FirstName, Is.EqualTo("Organizer"));
            Assert.That(member.LastName, Is.EqualTo("Name"));
        }
    }
}