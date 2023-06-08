using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Commands
{
    public record CreateTeamCommand(CreateTeamDTO dto) : IRequest<ErrorOr<Team>>;
}
