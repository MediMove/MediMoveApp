using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Authentication.Commands;

/// <summary>
/// Command for registering a new admin.
/// </summary>
/// <param name="Request"><see cref="RegisterAdminRequest"/></param>
public record RegisterAdminCommand(RegisterAdminRequest Request) : IRequest<ErrorOr<User>>;