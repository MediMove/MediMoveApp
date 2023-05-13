using FluentValidation;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Validators
{
    public class CreateTransportCommandValidator : AbstractValidator<CreateTransportDTO>
    {
        public CreateTransportCommandValidator()
        {
            RuleFor( x => x.PatientId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.StartTime)
                .Must(startTime => startTime > DateTime.Now);

            RuleFor(x => x.Financing)
                .IsInEnum();

            RuleFor(x => x.Destination)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.PatientPosition)
                .IsInEnum();

            RuleFor(x => x.TransportType)
                .IsInEnum();
        }
    }
}
