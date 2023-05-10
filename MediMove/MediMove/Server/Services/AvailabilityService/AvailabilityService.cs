using AutoMapper;
using MediMove.Server.Exceptions;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs.temp;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Services.AvailabilityService
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IMapper _mapper;
        private readonly IParamedicRepository _paramedicRepository;

        public AvailabilityService(IAvailabilityRepository availabilityRepository, IMapper mapper, IParamedicRepository paramedicRepository)
        {
            _availabilityRepository = availabilityRepository;
            _mapper = mapper;
            _paramedicRepository = paramedicRepository;
        }

        public async Task<AvailabilityDTO> GetById(int id)
        {
            var availability = await _availabilityRepository.GetAvailability(id) ?? throw new NotFoundException("No availabilities found.");
            var availabilityDTO = _mapper.Map<AvailabilityDTO>(availability);

            return availabilityDTO;
        }

        public async Task<IEnumerable<AvailabilityDTO>> GetByParamedic(int id)
        {
            var availabilities = await _availabilityRepository.GetByParamedic(id);
            var availabilitiesDTO = _mapper.Map<IEnumerable<AvailabilityDTO>>(availabilities);

            return availabilitiesDTO;
        }

        public async Task BulkCreate(int id, IEnumerable<ShiftType> shifts)
        {
            throw new NotImplementedException();
        }
    }
}
