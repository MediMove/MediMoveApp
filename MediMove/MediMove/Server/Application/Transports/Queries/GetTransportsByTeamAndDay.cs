using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Queries;
public record GetTransportsByTeamAndDayQuery(int TeamId, DateTime Day) : IRequest<ErrorOr<GetTransportsResponse>>;
