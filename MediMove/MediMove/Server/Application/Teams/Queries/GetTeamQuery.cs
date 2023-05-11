using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Queries.GetTeamQuery;

public record GetTeamQuery(int Id) : IRequest<ErrorOr<TeamDTO>>;


