﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto;
using Tnf.Events.Bus;
using Tnf.Architecture.Domain.Events.WhiteHouse;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Localization;

namespace Tnf.Architecture.Domain.WhiteHouse
{
    internal class WhiteHouseService : DomainService<IWhiteHouseRepository>, IWhiteHouseService
    {
        private readonly IEventBus _eventBus;

        public WhiteHouseService(IWhiteHouseRepository repository,
            IEventBus eventBus)
            : base(repository)
        {
            _eventBus = eventBus;
        }

        public async Task<DtoResponseBase> DeletePresidentAsync(string id)
        {
            var success = await Repository.DeletePresidentsAsync(id);

            var result = new DtoResponseBase();

            return result;
        }

        public Task<PagingResponseDto<PresidentDto>> GetAllPresidents(GellAllPresidentsDto request)
        {
            return Repository.GetAllPresidents(request);
        }

        public async Task<PresidentDto> GetPresidentById(string id)
        {
            return await Repository.GetPresidentById(id);
        }

        public async Task<DtoResponseBase<List<PresidentDto>>> InsertPresidentAsync(List<PresidentDto> presidents, bool sync = false)
        {
            var response = new DtoResponseBase<List<PresidentDto>>();

            foreach (var item in presidents)
            {
                var builder = new PresidentBuilder()
                   .WithId(item.Id)
                   .WithName(item.Name)
                   .WithZipCode(item.ZipCode);

                var build = builder.Build();
                if (!build.Success)
                    response.AddNotifications(build.Notifications);
            }

            if (response.Success)
            {
                presidents = await Repository.InsertPresidentsAsync(presidents, sync);
                response.Data = presidents;

                // Trigger president created event
                presidents.ForEach((president) => _eventBus.Trigger(new PresidentCreatedEvent(president)));
            }

            return response;
        }

        public async Task<DtoResponseBase<PresidentDto>> UpdatePresidentAsync(PresidentDto president)
        {
            var response = new DtoResponseBase<PresidentDto>();

            var builder = new PresidentBuilder()
                .WithId(president.Id)
                .WithName(president.Name)
                .WithZipCode(president.ZipCode);
            
            var build = builder.Build();
            if (!build.Success)
                response.AddNotifications(build.Notifications);

            if (response.Success)
                response.Data = await Repository.UpdatePresidentsAsync(president);

            return response;
        }
    }
}
