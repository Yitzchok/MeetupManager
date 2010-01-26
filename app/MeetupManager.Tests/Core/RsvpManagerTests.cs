using System.Collections.Generic;
using System.IO;
using MeetupManager.Core.Domain;
using MeetupManager.Core.JDto;
using MeetupManager.Core.Repositories;
using MeetupManager.Core.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Linq;
using Rhino.Mocks;

namespace Tests.MeetupManager.Core
{
    public class RsvpManagerTests : TestFixtureBase
    {
        private RsvpManager manager;
        public IList<RsvpItem> RsvpItems { get; set; }

        [SetUp]
        public void SetUp()
        {
            manager = new RsvpManager();

            IMeetupService meetupService = new MeetupService(GetRepository());
            RsvpItems = meetupService.GetRsvpsForEvent(eventId);
        }

        [Test]
        public void Should_return_attendee_for_each_guest()
        {
            Assert.AreEqual(4, manager.GetAttendees(RsvpItems).Count());
        }

        [Test]
        public void Should_have_correct_name()
        {
            List<Attendee> attendees = manager.GetAttendees(RsvpItems).ToList();


            Assert.AreEqual("Bill", attendees[0].FirstName);
            Assert.AreEqual("Gates", attendees[0].LastName);
            Assert.AreEqual("Bill Gates", attendees[0].RSVPName);
            Assert.AreEqual("Bill Gates", attendees[0].RSVPAnswer);


            Assert.AreEqual("Barak", attendees[1].FirstName);
            Assert.AreEqual("Obama", attendees[1].LastName);
            Assert.AreEqual("Barak Obama", attendees[1].RSVPName);
            Assert.AreEqual("Barak Obama, Mohamed Ahmadinejad", attendees[1].RSVPAnswer);


            Assert.AreEqual("Mohamed", attendees[2].FirstName);
            Assert.AreEqual("Ahmadinejad", attendees[2].LastName);
            Assert.AreEqual("Barak Obama", attendees[2].RSVPName);
            Assert.AreEqual("Barak Obama, Mohamed Ahmadinejad", attendees[2].RSVPAnswer);
        }


        [Test]
        public void if_the_person_has_three_names__first_two_goes_to_firstname()
        {
            Attendee attendee = manager.GetAttendee(new RsvpItem { Name = "Some ones names" }, 0);
            Assert.AreEqual("Some ones", attendee.FirstName);
            Assert.AreEqual("names", attendee.LastName);
        }

        [Test]
        public void can_split_on_name_spaces()
        {
            RsvpItem item = new RsvpItem { Name = "first last", Answers = new []{ "First Name Other Name Name Last" }, Guests = 2 };
            Attendee attendee = manager.GetAttendee(item, 0);
            Assert.AreEqual("First", attendee.FirstName);
            Assert.AreEqual("Name", attendee.LastName);

            Attendee attendee1 = manager.GetAttendee(item, 1);
            Assert.AreEqual("Other", attendee1.FirstName);
            Assert.AreEqual("Name", attendee1.LastName);

            Attendee attendee2 = manager.GetAttendee(item, 2);
            Assert.AreEqual("Name", attendee2.FirstName);
            Assert.AreEqual("Last", attendee2.LastName);
        }


        long eventId = 10458456L;
        private IMeetupRepository GetRepository()
        {
            IMeetupRepository repository = S<IMeetupRepository>();
            repository.Expect(x => x.GetRsvpsForEvent(eventId)).Return(GetRsvps());
            return repository;
        }

        private RsvpJDto GetRsvps()
        {
            return JsonConvert.DeserializeObject<RsvpJDto>(File.ReadAllText("TestData/rsvps.json"));
        }
    }
}