using FluentValidation;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using MediMove.Shared.Validators;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Validators
{
    public class CreateTransportCommandValidator : AbstractValidator<CreateTransportCommand>
    {
        public CreateTransportCommandValidator(MediMoveDbContext _dbContext)
        {
            RuleFor(x => x.Dto.PatientId)
                .NotEmpty()
                .GreaterThan(0)
                .CustomAsync(async (patientId, context, cancellationToken) =>
                {
                    if (!await _dbContext.Patients.AnyAsync(p=> p.Id == patientId, cancellationToken))
                        context.AddFailure("PatientId", "Patient does not exits");
                });

            RuleFor(x => x)
                .CustomAsync(async (x, context, cancellationToken) =>
                {
                    if (x.Dto.TeamId is not null)
                    {
                        var team = await _dbContext.Teams
                            .FirstOrDefaultAsync(t => t.Id == x.Dto.TeamId,cancellationToken);

                        if (team is null)
                        {
                            context.AddFailure("TeamId", "Team does not exits");
                            return;
                        }

                        if (x.Dto.StartTime.ToShiftType() != team.ShiftType)
                        {
                            context.AddFailure("TeamId", "Team is on different shift than transport.");
                            return;
                        }
                    }
                });

            RuleFor(x => x.Dto.StartTime)
                .Must(startTime => AvailabilityValidatiors.CanExecuteCommands(startTime,startTime.ToShiftType().Value))
                .WithMessage("Start time must be before current shift.");

            RuleFor(x => x.Dto.Financing)
                .Must(x => Enum.IsDefined(typeof(Financing), x)).WithMessage("Incorrect financing type."); ;

            RuleFor(x => x.Dto.Destination)
                .NotEmpty()
                .MaximumLength(70);

            RuleFor(x => x.Dto.PatientPosition)
                .Must(x => Enum.IsDefined(typeof(PatientPosition), x)).WithMessage("Incorrect patient position.");

            RuleFor(x => x.Dto.TransportType)
                .Must(x => Enum.IsDefined(typeof(TransportType), x)).WithMessage("Incorrect transport type.");
        }
    }
}
