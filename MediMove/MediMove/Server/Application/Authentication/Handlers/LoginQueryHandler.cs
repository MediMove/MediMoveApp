using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Authentication.Queries;
using MediMove.Server.Application.Errors;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;

namespace MediMove.Server.Application.Authentication.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<string>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public LoginQueryHandler(MediMoveDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public async Task<ErrorOr<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();

            var user = await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == request.dto.Email);

            if (user is null) 
                return Errors.Errors.LoginError;


            var verificationResult =
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.dto.Password);

            if(verificationResult == PasswordVerificationResult.Failed)
                return Errors.Errors.LoginError;

            if(!IsUserWorking(user))
                return Errors.Errors.LoginError;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("AccountId", $"{user.AccountId}"),
                new Claim(ClaimTypes.Role,$"{user.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddHours(_authenticationSettings.JwtExpireHours);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials:credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public bool IsUserWorking(User user)
        {
            if (user.AccountId == null)
                return true;

            switch (user.RoleId)
            {
                case 2:
                    var paramedic = _dbContext.Paramedics.FirstOrDefault(p => p.Id == user.AccountId);
                    return paramedic?.IsWorking ?? false;

                case 3:
                    var dispatcher = _dbContext.Dispatchers.FirstOrDefault(d => d.Id == user.AccountId);
                    return dispatcher?.IsWorking ?? false;

                default:
                    return false;
            }
        }
    }
}
