using System.Data;
using FluentValidation;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Data;

namespace MediMove.Server.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(MediMoveDbContext _dbContext)
        {
            RuleFor(x => x.dto.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.dto.RoleId).NotEmpty();
            RuleFor(x => x.dto.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = _dbContext.Users.Any(u => u.Email == value);
                    if(emailInUse) context.AddFailure("Email", "That email is in use");
                });
        }
    }
}
