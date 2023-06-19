using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Employees.Commands;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Employees.Handlers
{
    public class PutParamedicsCommandHandler : IRequestHandler<PutParamedicsCommand, ErrorOr<Paramedic[]>>
    {
        private readonly MediMoveDbContext _dbContext;
        private readonly IMapper _mapper;

        public PutParamedicsCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<Paramedic[]>> Handle(PutParamedicsCommand command, CancellationToken cancellationToken)
        {
            var paramedicDTOs = command.Paramedics.OrderBy(d => d.Id).ToArray();
            var paramedicIds = command.Paramedics.Select(d => d.Id);

            var paramedics = await _dbContext.Paramedics
                .Include(d => d.PersonalInformation)
                .Include(d => d.Rates)
                .Where(d => paramedicIds.Contains(d.Id))
                .OrderBy(d => d.Id)
                .ToArrayAsync(cancellationToken);

            if (paramedics.Length != command.Paramedics.Length)
                return Errors.Errors.EntityNotFoundError;

            var users = await _dbContext.Users
                .Where(u => u.Role.Name == "Paramedic" && paramedicIds.Contains(u.AccountId.Value))
                .OrderBy(u => u.AccountId)
                .ToArrayAsync(cancellationToken);

            if (users.Length != command.Paramedics.Length)
                return Errors.Errors.EntityNotFoundError;

            for (int i = 0; i < paramedics.Length; i++)
            {
                var paramedic = paramedics[i];
                var paramedicDTO = command.Paramedics[i];

                _mapper.Map(paramedicDTO, paramedic);
                users[i].Email = paramedicDTO.Email;

                var currentSalary = paramedic.Rates
                    .OrderByDescending(r => r.Date)
                    .Select(r => r.PayPerHour)
                    .First();

                if (currentSalary == paramedicDTO.PayPerHour)
                    continue;

                paramedic.Rates.Add(new Rate
                {
                    Date = DateTime.Now,
                    PayPerHour = paramedicDTO.PayPerHour
                });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return paramedics;
        }
    }
}