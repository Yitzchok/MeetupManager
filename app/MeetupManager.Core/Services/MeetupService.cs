using System;
using System.Collections.Generic;
using AutoMapper;
using MeetupManager.Core.Domain;
using MeetupManager.Core.JDto;
using MeetupManager.Core.Repositories;

namespace MeetupManager.Core.Services
{
    public class MeetupService : IMeetupService
    {
        private readonly IMeetupRepository repository;

        public MeetupService(IMeetupRepository repository)
        {
            this.repository = repository;
        }

        public IList<RsvpItem> GetRsvpsForEvent(long eventId)
        {
            var dto = repository.GetRsvpsForEvent(eventId);
            Mapper.CreateMap<RsvpJDto.RsvpItemJDto, RsvpItem>();

            var item = Mapper.Map<IList<RsvpJDto.RsvpItemJDto>, IList<RsvpItem>>(dto.Results);

            return item;
        }
    }
}