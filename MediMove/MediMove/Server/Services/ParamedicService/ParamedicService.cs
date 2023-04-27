using AutoMapper;
using MediMove.Server.Exceptions;
using MediMove.Server.Models;
using MediMove.Server.Repositories;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.DTOs.temp;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Services.ParamedicService
{
    public class ParamedicService : IParamedicService
    {
        private readonly IParamedicRepository _paramedicRepository;
        private readonly IMapper _mapper;

        public ParamedicService(IParamedicRepository ParamedicRepository, IMapper mapper)
        {
            _paramedicRepository = ParamedicRepository;
            _mapper = mapper;
        }

        public async Task<ParamedicDTO> GetById(int id)
        {
            var paramedic = await _paramedicRepository.GetParamedic(id) ?? throw new NotFoundException("No paramedics found.");
            var paramedicDTO = _mapper.Map<ParamedicDTO>(paramedic);

            return paramedicDTO;
        }

        public async Task<IEnumerable<ParamedicDTO>> GetAll()
        {
            var paramedics = await _paramedicRepository.GetParamedics();
            var paramedicsDTO = _mapper.Map<IEnumerable<ParamedicDTO>>(paramedics);

            return paramedicsDTO;
        }

        public async Task Create(CreateParamedicDTO dto)
        {
            var newParamedic = _mapper.Map<Paramedic>(dto);

            await _paramedicRepository.Create(newParamedic);
        }
    }
}
