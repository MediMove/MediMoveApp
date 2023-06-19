using FluentValidation;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Authentication.Validators
{
    /// <summary>
    /// Validator for <see cref="RegisterUserCommand"/>.
    /// </summary>
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        /// <summary>
        /// Constructor for <see cref="RegisterUserCommandValidator"/>.
        /// </summary>
        /// <param name="_dbContext"><see cref="MediMoveDbContext"/></param>
        public RegisterUserCommandValidator(MediMoveDbContext _dbContext)
        {
            RuleFor(command => command.Email)
                .Custom(async (email, context) =>
                {
                    if (await _dbContext.Users.AnyAsync(u => u.Email == email))
                        context.AddFailure("Email", "Email already in use");
                })
                .When(command => command.Email.IsValidEmail())
                .Must(email => email.IsValidEmail()).WithMessage("Invaild {PropertyName}");

            RuleFor(x => x.Password)
                .Must(e => e.IsValidPassword()).WithMessage("Invaild {PropertyName}");
        }
    }
}