using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Paramedics.Queries.GetParamedicQuery;

public record GetParamedicDTO(int Id) : IRequest<ErrorOr<ParamedicDTO>>;
public class GetParamedicQueryHandler : IRequestHandler<GetParamedicDTO, ErrorOr<ParamedicDTO>>
{
    private readonly IMapper _mapper;
    private readonly IParamedicRepository _paramedicRepository;

    public GetParamedicQueryHandler(IMapper mapper, IParamedicRepository paramedicRepository)
    {
        _mapper = mapper;
        _paramedicRepository = paramedicRepository;
    }

    public async Task<ErrorOr<ParamedicDTO>> Handle(GetParamedicDTO request, CancellationToken cancellationToken)
    {
        var paramedic = await _paramedicRepository.GetParamedic(request.Id);

        if (paramedic is null)
            return Errors.Errors.EntityNotFoundError;

        var paramedicDTO = _mapper.Map<ParamedicDTO>(paramedic);

        if (paramedicDTO is null)
            return Errors.Errors.MappingError;

        return paramedicDTO;
    }
}
