using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;

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
                .Must(startTime => startTime >= DateTime.Now).WithMessage("Date must be in the future");

            RuleFor(x => x.Dto.Financing).NotEmpty()
                .Must(x => Enum.IsDefined(typeof(Financing), x)).WithMessage("Incorrect financing type"); ;

            RuleFor(x => x.Dto.Destination)
                .NotEmpty()
                .MaximumLength(70);

            RuleFor(x => x.Dto.PatientPosition).NotEmpty()
                .Must(x => Enum.IsDefined(typeof(PatientPosition), x)).WithMessage("Incorrect patient position");

            RuleFor(x => x.Dto.TransportType).NotEmpty()
                .Must(x => Enum.IsDefined(typeof(TransportType), x)).WithMessage("Incorrect transport type");

            RuleFor(x => x.Dto.FirstName).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2,25);

            RuleFor(x => x.Dto.LastName).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 25);

            RuleFor(x => x.Dto.StreetAddress).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 30);

            RuleFor(x => x.Dto.HouseNumber).NotEmpty()
                .Matches(@"^[a-zA-Z0-9\s-]+$").Length(1, 10);

            RuleFor(x => x.Dto.ApartmentNumber).LessThanOrEqualTo(200);

            RuleFor(x => x.Dto.PostalCode).NotEmpty()
                .Matches(@"^\d{2}-\d{3}(\d{2})?$");

            RuleFor(x => x.Dto.StateProvince).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 30);

            RuleFor(x => x.Dto.City).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 30);

            RuleFor(x => x.Dto.Country).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 30);

            RuleFor(x => x.Dto.PhoneNumber).NotEmpty()
                .Matches(@"^[0-9]+([- ]?[0-9]+)*$").Length(2, 30);

            RuleFor(x => x.Dto.BankAccountNumber).NotEmpty()
                .Length(6, 60);

            RuleFor(x => x.Dto.InvoiceDate)
                .Must(invoiceDate => invoiceDate >= DateTime.Now);
        }
    }
    
} 
    
