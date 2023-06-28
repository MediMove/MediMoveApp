using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;
using MediMove.Shared.Validators;

namespace MediMove.Server.Application.Transports.Validators
{
    public class CreateTransportWithBillingValidator : AbstractValidator<CreateTransportWithBillingCommand>
    {
        public CreateTransportWithBillingValidator(MediMoveDbContext _dbContext)
        {
            RuleFor(x => x.Dto.PatientId)
                .NotEmpty()
                .GreaterThan(0)
                .CustomAsync(async (patientId, context, cancellationToken) =>
                {
                    if (!await _dbContext.Patients.AnyAsync(p => p.Id == patientId, cancellationToken))
                        context.AddFailure("PatientId", "Patient does not exits");
                });

            RuleFor(x => x)
                .CustomAsync(async (x, context, cancellationToken) =>
                {
                    if (x.Dto.TeamId is not null)
                    {
                        var team = await _dbContext.Teams
                            .FirstOrDefaultAsync(t => t.Id == x.Dto.TeamId, cancellationToken);

                        if (team is null)
                        {
                            context.AddFailure("TeamId", "Team does not exits");
                            return;
                        }

                        if (x.Dto.StartTime.ToShiftType() != team.ShiftType)
                        {
                            context.AddFailure("TeamId", "Team is on different shift than transport");
                            return;
                        }
                    }
                });

            RuleFor(x => x.Dto.StartTime)
                .Must(startTime => AvailabilityValidatiors.CanExecuteCommands(startTime, startTime.ToShiftType().Value))
                .WithMessage("Start time must be before current shift.");

            RuleFor(x => x.Dto.Financing)
                .Must(x => Enum.IsDefined(typeof(Financing), x)).WithMessage("Incorrect financing type"); ;

            RuleFor(x => x.Dto.Destination)
                .NotEmpty()
                .MaximumLength(100).WithMessage("Provide valid destination.");

            RuleFor(x => x.Dto.PatientPosition)
                .Must(x => Enum.IsDefined(typeof(PatientPosition), x)).WithMessage("Incorrect patient position.");

            RuleFor(x => x.Dto.TransportType)
                .Must(x => Enum.IsDefined(typeof(TransportType), x)).WithMessage("Incorrect transport type.");

            RuleFor(x => x.Dto.FirstName).NotEmpty()
                .Must(x => x.IsValidFirstName()).WithMessage("Provide valid first name.");

            RuleFor(x => x.Dto.LastName).NotEmpty()
                .Must(x => x.IsValidLastName()).WithMessage("Provide valid last name.");

            RuleFor(x => x.Dto.StreetAddress).NotEmpty()
                .Must(x => x.IsValidStreetAddress()).WithMessage("Provide valid street address.");

            RuleFor(x => x.Dto.HouseNumber).NotEmpty()
                .Must(x => x.IsValidHouseNumber()).WithMessage("Provide valid house number.");

            RuleFor(x => x.Dto.ApartmentNumber)
                .Must(x => x.IsValidApartmentNumber()).WithMessage("Provide valid apartment number.");

            RuleFor(x => x.Dto.PostalCode).NotEmpty()
                .Must(x => x.IsValidPostalCode()).WithMessage("Provide valid postal code.");

            RuleFor(x => x.Dto.StateProvince).NotEmpty()
                .Must(x => x.IsValidStateProvince()).WithMessage("Provide valid state/province.");

            RuleFor(x => x.Dto.City).NotEmpty()
                .Must(x => x.IsValidCity()).WithMessage("Provide valid city.");

            RuleFor(x => x.Dto.Country).NotEmpty()
                .Must(x => x.IsValidCountry()).WithMessage("Provide valid country.");

            RuleFor(x => x.Dto.PhoneNumber).NotEmpty()
                .Must(x => x.IsValidPhoneNumber()).WithMessage("Provide valid phone number.");

            RuleFor(x => x.Dto.BankAccountNumber).NotEmpty()
                .Must(x => x.IsValidBankAccountNumber()).WithMessage("Provide valid account number.");

            RuleFor(x => x.Dto.InvoiceDate)
                .Must(invoiceDate => invoiceDate >= DateTime.Today);
        }
    }
    
} 
    
