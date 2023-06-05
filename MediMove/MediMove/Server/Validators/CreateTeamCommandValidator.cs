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
            RuleFor(x => x.dto.ParamedicId).GreaterThan(0);

            RuleFor(x => x.dto.DriverId).GreaterThan(0)
                .Custom((driverIdValue, context) =>
                {
                    var isDriver = _dbContext.Paramedics.FirstOrDefault(p => p.Id == driverIdValue).IsDriver;
                    if (!isDriver) context.AddFailure("DriverId", $"Paramedic provided as driver (ID:{driverIdValue}) is not a driver");
                });

            RuleFor(x => x.dto.Day).Must(day => day > DateTime.Today);

            RuleFor(x => x.dto)
                .Custom((dto, context) =>
                {
                    var isAlreadyPresentThatDay = _dbContext.Teams
                        .Where(t => t.Day == dto.Day)
                        .Any(t => 
                            t.DriverId == dto.DriverId || 
                            t.ParamedicId == dto.DriverId
                        );

                    if (isAlreadyPresentThatDay) context.AddFailure("DriverId", $"Driver provided (ID:{dto.DriverId}) is already working that day");

                })
                .Custom((dto, context) =>
                {
                    var isAlreadyPresentThatDay = _dbContext.Teams
                        .Where(t => t.Day == dto.Day)
                        .Any(t =>
                            t.ParamedicId == dto.ParamedicId ||
                            t.DriverId == dto.ParamedicId
                        );

                    if (isAlreadyPresentThatDay) context.AddFailure("ParamedicId", $"Paramedic provided (ID:{dto.ParamedicId}) is already working that day");

                });
        }
    }
}

