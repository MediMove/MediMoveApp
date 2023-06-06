using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Validators
{
    public class AddTransportTeamCommandValidator : AbstractValidator<AddTransportTeamCommand>
    {
        public AddTransportTeamCommandValidator(MediMoveDbContext _dbContext)
        {
            RuleFor(x => x.TeamId).GreaterThan(0);
            RuleFor(x => x.TransportId).GreaterThan(0);

            RuleFor(x=> x)
                .CustomAsync(async (x, context, cancellationToken) =>
                {
                    var transport = await _dbContext.Transports.FirstOrDefaultAsync(t => t.Id == x.TransportId);
                    var team = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == x.TeamId);

                    var isSameDay = team.Day.Day == transport.StartTime.Day &&
                                    team.Day.Month == transport.StartTime.Month &&
                                    team.Day.Year == transport.StartTime.Year;

                    if (!isSameDay) context.AddFailure("TeamId", "Date of transport and date when team is working is different");
                });
        }
    }
}
