using AutoMapper;
using MediMove.Server.Exceptions;
using MediMove.Server.Models;
using MediMove.Server.Repositories;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Services.DispatcherService
{
    public class DispatcherService : IDispatcherService
    {
        private readonly IDispatcherRepository _dispatcherRepository;
        private readonly IMapper _mapper;

        public DispatcherService(IDispatcherRepository DispatcherRepository, IMapper mapper)
        {
            _dispatcherRepository = DispatcherRepository;
            _mapper = mapper;
        }

        public async Task<DispatcherDTO> GetById(int id)
        {
            var dispatcher = await _dispatcherRepository.GetDispatcher(id) ?? throw new NotFoundException("No dispatchers found.");
            var dispatcherDTO = _mapper.Map<DispatcherDTO>(dispatcher);

            return dispatcherDTO;
        }

        public async Task<IEnumerable<DispatcherDTO>> GetAll()
        {
            var dispatchers = await _dispatcherRepository.GetDispatchers();
            var dispatchersDTO = _mapper.Map<IEnumerable<DispatcherDTO>>(dispatchers);

            return dispatchersDTO;
        }

        public async Task Create(CreateDispatcherDTO dto)
        {
            var newDispatcher = _mapper.Map<Dispatcher>(dto);

            await _dispatcherRepository.Create(newDispatcher);
        }
    }
}
