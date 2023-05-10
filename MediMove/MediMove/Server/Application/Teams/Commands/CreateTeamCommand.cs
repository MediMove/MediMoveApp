using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Commands
{
    public class CreateTeamCommand : IRequestHandler<CreateTeamDTO, ErrorOr<int>>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommand(IMapper mapper, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<ErrorOr<int>> Handle(CreateTeamDTO request, CancellationToken cancellationToken)
        {
            var team = _mapper.Map<Team>(request);

            if (team is null)
                return Errors.Errors.MappingError;

            // TODO: Check if team is valid
            // _teamRepository.Update(team);

            return team.Id;
        }
    }
}
