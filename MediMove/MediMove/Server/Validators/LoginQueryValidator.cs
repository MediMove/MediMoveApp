﻿using FluentValidation;
using MediMove.Server.Application.Authentication.Queries;

namespace MediMove.Server.Validators
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.dto.Email).NotEmpty();
            RuleFor(x => x.dto.Password).NotEmpty();
        }
    }
}
