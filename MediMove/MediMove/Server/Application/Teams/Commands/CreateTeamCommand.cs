using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Commands
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamDTO, ErrorOr<int>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public CreateTeamCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<int>> Handle(CreateTeamDTO request, CancellationToken cancellationToken)
        {
            var team = _mapper.Map<Team>(request);

            if (team is null)
                return Errors.Errors.MappingError;

            // TODO: Add validation

            await _dbContext.Teams.AddAsync(team, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return team.Id;
        }
    }
}
