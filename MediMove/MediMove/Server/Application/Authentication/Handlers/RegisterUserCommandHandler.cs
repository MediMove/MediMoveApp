using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Data;
using MediMove.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Authentication.Handlers
{

    
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<int>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserCommandHandler(IMapper mapper, MediMoveDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<ErrorOr<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = _mapper.Map<User>(request.dto);

            if (newUser is null)
                return Errors.Errors.MappingError;

            var hashedPassword = _passwordHasher.HashPassword(newUser, request.dto.Password);
            newUser.PasswordHash = hashedPassword;

            await _dbContext.Users.AddAsync(newUser, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newUser.Id;

        }
    }
}
