using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Authentication.Commands;

/// <summary>
/// Command for registering a dispatcher.
/// </summary>
/// <param name="Request"><see cref="RegisterDispatcherRequest"/></param>
public record RegisterDispatcherCommand(RegisterDispatcherRequest Request) : IRequest<ErrorOr<Dispatcher>>;