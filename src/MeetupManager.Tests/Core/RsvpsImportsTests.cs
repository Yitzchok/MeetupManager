using System;
using System.Collections.Generic;
using System.IO;
using MeetupManager.Core.Domain;
using MeetupManager.Core.JDto;
using MeetupManager.Core.Repositories;
using MeetupManager.Core.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.MeetupManager.Core
{
    public class RsvpsImportsTests : TestFixtureBase
    {
        long eventId = 10458456L;
        [Test]
        public void Can_get_all_rsvps_for_event()
        {
            IMeetupService meetupService = new MeetupService(GetRepository());
            IList<RsvpItem> rsvps = meetupService.GetRsvpsForEvent(eventId);

            Assert.AreEqual(3, rsvps.Count);
        }

        [Test]
        public void Name_should_be_populated()
        {
            IMeetupService meetupService = new MeetupService(GetRepository());
            IList<RsvpItem> rsvps = meetupService.GetRsvpsForEvent(eventId);

            var gates = rsvps[0];
            Assert.AreEqual("Bill Gates", gates.Name);
            Assert.AreEqual("Bill Gates", gates.Answers[0]);
            var obama = rsvps[1];
            Assert.AreEqual("Barak Obama", obama.Name);
            Assert.AreEqual("Barak Obama", obama.Answers[0].Split(',')[0].Trim());
            Assert.AreEqual("Mohamed Ahmadinejad", obama.Answers[0].Split(',')[1].Trim());
        }

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