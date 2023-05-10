using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Paramedics.Queries.GetAllParamedicsQuery;

public record GetAllParamedicsDTO : IRequest<ErrorOr<IEnumerable<ParamedicDTO>>>;
public class GetAllParamedicsQueryHandler : IRequestHandler<GetAllParamedicsDTO, ErrorOr<IEnumerable<ParamedicDTO>>>
{
    private readonly IMapper _mapper;
    private readonly IParamedicRepository _paramedicRepository;

    public GetAllParamedicsQueryHandler(IMapper mapper, IParamedicRepository paramedicRepository)
    {
        _mapper = mapper;
        _paramedicRepository = paramedicRepository;
    }

    public async Task<ErrorOr<IEnumerable<ParamedicDTO>>> Handle(GetAllParamedicsDTO request, CancellationToken cancellationToken)
    {
        var paramedics = await _paramedicRepository.GetParamedics();
        var paramedicDTOs = _mapper.Map<IEnumerable<ParamedicDTO>>(paramedics);

        if (paramedicDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(paramedicDTOs);
    }
}
