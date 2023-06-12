using FluentValidation;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.DTOs;
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
                    if (!await _dbContext.Patients.AnyAsync(p=> p.Id == patientId))
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
                            context.AddFailure("TeamId", "Team is on different shift than transport");
                            return;
                        }
                    }
                });

            //RuleFor(x => x.Dto.StartTime)
            //    .Must(startTime => startTime > DateTime.Now);

            RuleFor(x => x.Dto.Financing)
                .IsInEnum();

            RuleFor(x => x.Dto.Destination)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Dto.PatientPosition)
                .IsInEnum();

            RuleFor(x => x.Dto.TransportType)
                .IsInEnum();
        }
    }
}
