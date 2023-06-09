﻿using FluentValidation;
using MediMove.Server.Application.Patients.Commands;
using MediMove.Server.Application.Shared;
using MediMove.Shared.Validators;

namespace MediMove.Server.Application.Patients.Validators
{
    /// <summary>
    /// Validator for <see cref="CreatePatientCommand"/>.
    /// </summary>
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        /// <summary>
        /// Constructor for <see cref="CreatePatientCommandValidator"/>.
        /// </summary>
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.Request.FirstName).FirstName();

            RuleFor(x => x.Request.LastName).LastName();

            RuleFor(x => x.Request.StreetAddress).StreetAddress();

            RuleFor(x => x.Request.HouseNumber).HouseNumber();

            RuleFor(x => x.Request.ApartmentNumber).ApartmentNumber();

            RuleFor(x => x.Request.PostalCode).PostalCode();

            RuleFor(x => x.Request.StateProvince).StateProvince();

            RuleFor(x => x.Request.City).City();

            RuleFor(x => x.Request.Country).Country();

            RuleFor(x => x.Request.PhoneNumber).PhoneNumber();

            RuleFor(x => x.Request.Weight).Must(i => i.IsValidWeight()).WithMessage("Invalid {PropertyName}");
        }
    }
}