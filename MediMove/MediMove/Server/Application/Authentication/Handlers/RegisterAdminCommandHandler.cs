using ErrorOr;
using MediatR;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Application.Models;

namespace MediMove.Server.Application.Authentication.Handlers;

/// <summary>
/// Handler for registering an admin.
/// </summary>
public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, ErrorOr<User>>
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor for <see cref="RegisterAdminCommandHandler"/>.
    /// </summary>
    /// <param name="mediator"></param>
    public RegisterAdminCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Method for handling <see cref="RegisterAdminCommand"/>.
    /// </summary>
    /// <param name="command"><see cref="RegisterAdminCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="User"/> wrapped in ErrorOr</returns>
    public async Task<ErrorOr<User>> Handle(RegisterAdminCommand command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new RegisterUserCommand(command.Request.Email, command.Request.Password, null, "Admin"),
            cancellationToken);
    }
}