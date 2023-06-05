using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Commands
{
    public record CreateTeamCommand(CreateTeamDTO dto) : IRequest<ErrorOr<Team>>;
}
