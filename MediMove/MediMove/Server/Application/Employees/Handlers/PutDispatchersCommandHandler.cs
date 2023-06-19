using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Employees.Commands;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MediMove.Server.Application.Employees.Handlers
{
    /// <summary>
    /// Handler for updating dispatchers.
    /// </summary>
    public class PutDispatchersCommandHandler : IRequestHandler<PutDispatchersCommand, ErrorOr<Dispatcher[]>>
    {
        private readonly MediMoveDbContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for <see cref="PutDispatchersCommandHandler"/>.
        /// </summary>
        /// <param name="mapper">mapper to inject</param>
        /// <param name="dbContext">dbContext to inject</param>
        public PutDispatchersCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling <see cref="PutDispatchersCommand"/>.
        /// </summary>
        /// <param name="command"><see cref="PutDispatchersCommand"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>array of updated Dispatchers</returns>
        public async Task<ErrorOr<Dispatcher[]>> Handle(PutDispatchersCommand command, CancellationToken cancellationToken)
        {
            var dispatcherDTOs = command.Dispatchers.OrderBy(d => d.Id).ToArray();
            var dispatcherIds = command.Dispatchers.Select(d => d.Id);

            var dispatchers = await _dbContext.Dispatchers
                .Include(d => d.PersonalInformation)
                .Include(d => d.Salaries)
                .Where(d => dispatcherIds.Contains(d.Id))
                .OrderBy(d => d.Id)
                .ToArrayAsync(cancellationToken);

            if (dispatchers.Length != command.Dispatchers.Length)
                return Errors.Errors.EntityNotFoundError;

            var users = await _dbContext.Users
                .Where(u => u.Role.Name == "Dispatcher" && dispatcherIds.Contains(u.AccountId.Value))
                .OrderBy(u => u.AccountId)
                .ToArrayAsync(cancellationToken);

            if (users.Length != command.Dispatchers.Length)
                return Errors.Errors.EntityNotFoundError;

            for (int i = 0; i < dispatchers.Length; i++)
            {
                var dispatcher = dispatchers[i];
                var dispatcherDTO = command.Dispatchers[i];

                _mapper.Map(dispatcherDTO, dispatcher);
                users[i].Email = dispatcherDTO.Email;

                var currentSalary = dispatcher.Salaries
                    .OrderByDescending(r => r.Date)
                    .Select(r => r.Income)
                    .FirstOrDefault();

                if (currentSalary == dispatcherDTO.Salary)
                    continue;

                dispatcher.Salaries.Add(new Salary
                {
                    Date = DateTime.Now,
                    Income = dispatcherDTO.Salary
                });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return dispatchers;
        }
    }
}