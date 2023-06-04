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
                .Custom((x, context) =>
                {
                    var transportDate = _dbContext.Transports.FirstOrDefault(t => t.Id == x.TransportId).StartTime;
                    var teamDate = _dbContext.Teams.FirstOrDefault(t => t.Id == x.TeamId).Day;

                    var isSameDay = teamDate.Day == transportDate.Day &&
                                    teamDate.Month == transportDate.Month &&
                                    teamDate.Year == transportDate.Year;

                    if (!isSameDay) context.AddFailure("TeamId", "Date of transport and date when team is working is different");
                });
        }
    }
}
