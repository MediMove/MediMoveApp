﻿using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Queries.GetTeamsQuery;
public record GetTeamsDTO : IRequest<ErrorOr<IEnumerable<TeamDTO>>>;
public class GetTeamsQueryHandler : IRequestHandler<GetTeamsDTO, ErrorOr<IEnumerable<TeamDTO>>>
{
    private readonly IMapper _mapper;
    private readonly ITeamRepository _teamRepository;

    public GetTeamsQueryHandler(IMapper mapper, ITeamRepository teamRepository)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
    }

    public async Task<ErrorOr<IEnumerable<TeamDTO>>> Handle(GetTeamsDTO request, CancellationToken cancellationToken)
    {
        var teams = await _teamRepository.GetTeams();
        var teamDTOs = _mapper.Map<IEnumerable<TeamDTO>>(teams);

        if (teamDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(teamDTOs);
    }
}
