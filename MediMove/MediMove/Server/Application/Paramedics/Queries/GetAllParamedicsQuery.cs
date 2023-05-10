using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Paramedics.Queries.GetAllParamedicsQuery;

public record GetAllParamedicsDTO : IRequest<ErrorOr<IEnumerable<ParamedicDTO>>>;
public class GetAllParamedicsQueryHandler : IRequestHandler<GetAllParamedicsDTO, ErrorOr<IEnumerable<ParamedicDTO>>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetAllParamedicsQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<IEnumerable<ParamedicDTO>>> Handle(GetAllParamedicsDTO request, CancellationToken cancellationToken)
    {
        var paramedics = await _dbContext.Paramedics.ToListAsync(cancellationToken: cancellationToken);

        var paramedicDTOs = _mapper.Map<IEnumerable<ParamedicDTO>>(paramedics);

        if (paramedicDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(paramedicDTOs);
    }
}
