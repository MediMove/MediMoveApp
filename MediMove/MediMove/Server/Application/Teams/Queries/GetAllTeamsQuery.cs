using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Teams.Queries.GetTeamsQuery;
public record GetAllTeamsDTO : IRequest<ErrorOr<IEnumerable<TeamDTO>>>;
public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsDTO, ErrorOr<IEnumerable<TeamDTO>>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetAllTeamsQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<IEnumerable<TeamDTO>>> Handle(GetAllTeamsDTO request, CancellationToken cancellationToken)
    {
        var teams = await _dbContext.Teams.ToListAsync(cancellationToken: cancellationToken);

        var teamDTOs = _mapper.Map<IEnumerable<TeamDTO>>(teams);

        if (teamDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(teamDTOs);
    }
}
