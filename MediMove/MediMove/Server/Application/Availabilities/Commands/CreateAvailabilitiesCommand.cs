using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Availabilities.Commands;

public record CreateAvailabilitiesCommand(CreateAvailabilitiesDTO Dto) : IRequest<ErrorOr<Unit>>;
    