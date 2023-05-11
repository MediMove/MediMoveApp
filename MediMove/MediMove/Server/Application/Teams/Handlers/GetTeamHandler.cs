using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Teams.Queries.GetTeamQuery;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Handlers
{
    public class GetTeamHandler : IRequestHandler<GetTeamQuery, ErrorOr<TeamDTO>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetTeamHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<TeamDTO>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var team = await _dbContext.Teams.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (team is null)
                return Errors.Errors.EntityNotFoundError;

            var teamDTO = _mapper.Map<TeamDTO>(team);

            if (teamDTO is null)
                return Errors.Errors.MappingError;

            return teamDTO;
        }
    }
}
