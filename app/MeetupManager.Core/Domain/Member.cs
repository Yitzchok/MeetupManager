using System;
using SharpArch.Core.DomainModel;

namespace MeetupManager.Core.Domain
{
    public class Member : Entity
    {
        public string MemberName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}