using System.Data;
using FluentValidation;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Data;

namespace MediMove.Server.Application.Authentication.Validators
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
                    if (emailInUse) context.AddFailure("Email", "That email is in use");
                });
            RuleFor(x => x.dto.Password).MinimumLength(8)
                .Custom((value, context) =>
                {
                    var passwordNotMatch = value !=
                                           context.InstanceToValidate.dto.ConfirmPassword;
                    if (passwordNotMatch)
                        context.AddFailure("Password", "Passwords do not match");
                });

            RuleFor(x => x.dto.RoleId).InclusiveBetween(0, 5);
        }
    }
}
