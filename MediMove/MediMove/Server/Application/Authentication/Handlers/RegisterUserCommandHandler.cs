using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Authentication.Handlers;

/// <summary>
/// Handler for registering a new user.
/// </summary>
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<User>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;

    /// <summary>
    /// Constructor for <see cref="RegisterUserCommandHandler"/>.
    /// </summary>
    /// <param name="mapper">mapper to inject</param>
    /// <param name="dbContext">dbContext to inject</param>
    /// <param name="passwordHasher">password hasher to inject</param>
    public RegisterUserCommandHandler(IMapper mapper, MediMoveDbContext dbContext, IPasswordHasher<User> passwordHasher)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Method for handling <see cref="RegisterUserCommand"/>.
    /// </summary>
    /// <param name="command"><see cref="RegisterUserCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="User"/> wrapped in ErrorOr</returns>
    public async Task<ErrorOr<User>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<User>(command);

        if (newUser is null)
            return Errors.Errors.MappingError;

        var role = await _dbContext.Roles
            .SingleOrDefaultAsync(r => r.Name == command.RoleName, cancellationToken);

        if (role == null)
        {
            role = new Role { Name = command.RoleName };
            await _dbContext.Roles.AddAsync(role, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        newUser.Role = role;
        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, command.Password);

        await _dbContext.Users.AddAsync(newUser, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newUser;
    }
}