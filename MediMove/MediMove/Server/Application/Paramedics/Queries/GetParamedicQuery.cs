using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Paramedics.Queries.GetParamedicQuery;

public record GetParamedicDTO(int Id) : IRequest<ErrorOr<ParamedicDTO>>;
public class GetParamedicQueryHandler : IRequestHandler<GetParamedicDTO, ErrorOr<ParamedicDTO>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetParamedicQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<ParamedicDTO>> Handle(GetParamedicDTO request, CancellationToken cancellationToken)
    {
        var paramedic = await _dbContext.Paramedics.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (paramedic is null)
            return Errors.Errors.EntityNotFoundError;

        var paramedicDTO = _mapper.Map<ParamedicDTO>(paramedic);

        if (paramedicDTO is null)
            return Errors.Errors.MappingError;

        return paramedicDTO;
    }
}
