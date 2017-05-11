﻿using Tnf.Application.Services;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Dto;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Services
{
    public class ProfessionalAppService : ApplicationService, IProfessionalAppService
    {
        private readonly IProfessionalService _service;

        public ProfessionalAppService(IProfessionalService service)
        {
            _service = service;
        }

        public PagingResponseDto<ProfessionalDto> GetAllProfessionals(GetAllProfessionalsDto dto) => _service.GetAllProfessionals(dto);

        public DtoResponseBase<ProfessionalDto> CreateProfessional(ProfessionalCreateDto dto) => _service.CreateProfessional(dto);

        public DtoResponseBase DeleteProfessional(ProfessionalKeysDto keys) => _service.DeleteProfessional(keys);

        public ProfessionalDto GetProfessional(ProfessionalKeysDto keys) => _service.GetProfessional(keys);

        public DtoResponseBase<ProfessionalDto> UpdateProfessional(ProfessionalDto dto) => _service.UpdateProfessional(dto);
    }
}
