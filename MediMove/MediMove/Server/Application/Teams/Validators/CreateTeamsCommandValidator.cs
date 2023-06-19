using FluentValidation;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Teams.Validators
{
    /// <summary>
    /// Validator for CreateTeamsCommand.
    /// </summary>
    public class CreateTeamsCommandValidator : AbstractValidator<CreateTeamsCommand>
    {
        /// <summary>
        /// Constructor for CreateTeamsCommandValidator.
        /// </summary>
        /// <param name="dbContext">MediMoveDbContext</param>
        public CreateTeamsCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(x => x.Request)
                .CustomAsync(async (request, context, cancellationToken) =>
                {
                    if (!TeamValidatiors.CanExecuteCommands(request.Date, request.Shift))
                    {
                        context.AddFailure("Request", "Cannot create teams for this date and shift");
                        return;
                    }

                    var ids = request.Teams.SelectMany(t => new[] { t.DriverId, t.ParamedicId }).ToHashSet();

                    if (ids.Count != request.Teams.Count * 2)
                    {
                        context.AddFailure("Request.Teams", "All IDs must be distinct");
                        return;
                    }

                    if (ids.Any(id => id <= 0))
                    {
                        context.AddFailure("Request.Teams", "All IDs must be positive");
                        return;
                    }

                    if (await dbContext.Teams
                            .AnyAsync(t => t.Day.Date == request.Date.Date &&
                                (ids.Contains(t.DriverId) || ids.Contains(t.ParamedicId)),
                            cancellationToken))
                    {
                        context.AddFailure("Request.Teams",
                                    "At least one paramedic is already assigned to team for this date");
                        return;
                    }

                    var relatedParamedicsDict = await dbContext.Paramedics
                        .Where(p => ids.Contains(p.Id) && p.IsWorking)
                        //.Include(p => p.Availabilities)
                        .SelectMany(p => p.Availabilities
                            .Where(a => a.Day.Date == request.Date.Date &&
                                (!a.ShiftType.HasValue || a.ShiftType == request.Shift)),
                            (p, a) => new { p.Id, p.IsDriver, a.ShiftType })
                        .ToDictionaryAsync(p => p.Id, cancellationToken);

                    if (relatedParamedicsDict.Count != ids.Count)
                    {
                        context.AddFailure("Request.Teams",
                            "At least one paramedic is invaild or does not have availability for a given day");
                        return;
                    }

                    if (request.Teams.Any(t => relatedParamedicsDict[t.DriverId].ShiftType.HasValue && relatedParamedicsDict[t.ParamedicId].ShiftType.HasValue &&
                        relatedParamedicsDict[t.DriverId].ShiftType != relatedParamedicsDict[t.ParamedicId].ShiftType))
                    {
                        context.AddFailure("Request.Teams", "Paramedic and driver have availability for different shifts");
                        return;
                    }

                    if (relatedParamedicsDict.Any(p => request.Teams
                        .Select(t => t.DriverId)
                        .ToHashSet()
                        .Contains(p.Value.Id) && !p.Value.IsDriver))
                            context.AddFailure("Request.Teams", "At least one assigned driver does not have driver license");
                })
                .When(command => command.Request != null
                    && command.Request.Teams != null
                    && command.Request.Teams.Any())
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.");

            When(command => command.Request != null, () =>
            {
                RuleFor(command => command.Request.Date.Date)
                    .NotEmpty().WithMessage("{PropertyName} cannot be empty");

                RuleFor(command => command.Request.Shift)
                    .IsInEnum().WithMessage("{PropertyName} with value {PropertyValue} must be a valid ShiftType");

                RuleFor(command => command.Request.Teams)
                    .NotEmpty().WithMessage("{PropertyName} cannot be empty");
            });
        }
    }
}