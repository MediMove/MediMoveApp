using FluentValidation;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Server.Data;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Validators
{
    /// <summary>
    /// Validator for the CreateTeamDTO
    /// </summary>
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        /// <summary>
        /// Validates the CreateTeamDTO
        /// </summary>
        public CreateTeamCommandValidator(MediMoveDbContext _dbContext)
        {
            RuleFor(x => x.dto.ParamedicId).GreaterThan(0)
                .MustAsync(async (id, cancellationToken) => await _dbContext.Paramedics.AnyAsync(p => p.Id == id)).WithMessage("Paramedic ID is incorrect.");

            RuleFor(x => x.dto.DriverId).GreaterThan(0)
                .CustomAsync(async (driverIdValue, context, cancellationToken) =>
                {
                    var driver = await _dbContext.Paramedics.FirstOrDefaultAsync(p => p.Id == driverIdValue);
                    if(driver is null) context.AddFailure("DriverId", "Driver ID is incorrect.");
                    else if (!driver.IsDriver) context.AddFailure("DriverId", $"Paramedic provided as driver (ID:{driverIdValue}) is not a driver.");
                });

            RuleFor(x => x.dto.Day).Must(day => day > DateTime.Today.AddDays(1)).WithMessage("Date must be in future.");

            RuleFor(x => x.dto)
                .CustomAsync(async (dto, context, cancellationToken) =>
                {
                    var isAlreadyPresentThatDay = await _dbContext.Teams
                        .Where(t => t.Day.Date == dto.Day.Date)
                        .AnyAsync(t => 
                            t.DriverId == dto.DriverId || 
                            t.ParamedicId == dto.DriverId
                        );

                    if (isAlreadyPresentThatDay) context.AddFailure("DriverId", $"Driver provided (ID:{dto.DriverId}) is already working that day.");

                })
                .CustomAsync(async (dto, context, cancellationToken) =>
                {
                    var isAlreadyPresentThatDay = await _dbContext.Teams
                        .Where(t => t.Day.Date == dto.Day.Date)
                        .AnyAsync(t =>
                            t.ParamedicId == dto.ParamedicId ||
                            t.DriverId == dto.ParamedicId
                        );

                    if (isAlreadyPresentThatDay) context.AddFailure("ParamedicId", $"Paramedic provided (ID:{dto.ParamedicId}) is already working that day.");

                });
        }
    }
}

