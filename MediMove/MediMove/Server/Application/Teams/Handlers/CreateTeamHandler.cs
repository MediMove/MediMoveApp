using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Handlers
{
    public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, ErrorOr<Team>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public CreateTeamHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<Team>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = _mapper.Map<Team>(request.dto);

            if (team is null)
                return Errors.Errors.MappingError;

            await _dbContext.Teams.AddAsync(team, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return team;
        }

 
    }
}
