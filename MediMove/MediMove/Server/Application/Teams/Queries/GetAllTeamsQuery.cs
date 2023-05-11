using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Teams.Queries;
public record GetAllTeamsQuery : IRequest<ErrorOr<IEnumerable<TeamDTO>>>;
