using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Availabilities.Commands;

public record CreateAvailabilitiesCommand(int ParamedicId, CreateAvailabilitiesDTO Dto) : IRequest<ErrorOr<Unit>>;
    