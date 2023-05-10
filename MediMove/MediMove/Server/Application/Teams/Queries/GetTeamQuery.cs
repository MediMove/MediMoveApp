using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Queries.GetTeamQuery;

public record GetTeamDTO(int Id) : IRequest<ErrorOr<TeamDTO>>;

public class GetTeamsQueryHandler : IRequestHandler<GetTeamDTO, ErrorOr<TeamDTO>>
{
    private readonly IMapper _mapper;
    private readonly ITeamRepository _teamRepository;

    public GetTeamsQueryHandler(IMapper mapper, ITeamRepository teamRepository)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
    }

    public async Task<ErrorOr<TeamDTO>> Handle(GetTeamDTO request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetTeam(request.Id);

        if (team is null)
            return Errors.Errors.EntityNotFoundError;

        var teamDTO = _mapper.Map<TeamDTO>(team);

        if (teamDTO is null)
            return Errors.Errors.MappingError;

        return teamDTO;
    }
}
