using ErrorOr;
using MediatR;
using MediMove.Server.Application.Employees.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Employees.Handlers
{
    /// <summary>
    /// Handler for getting all dispatchers.
    /// </summary>
    public class GetAllDispatchersQueryHandler : IRequestHandler<GetAllDispatchersQuery, ErrorOr<GetAllDispatchersResponse>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetAllEmployeesQueryHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public GetAllDispatchersQueryHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling GetAllDispatchersQuery.
        /// </summary>
        /// <param name="query">GetAllDispatchersQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetAllDispatchersResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetAllDispatchersResponse>> Handle(GetAllDispatchersQuery query, CancellationToken cancellationToken)
        {
            var dispatcherDTOs = await _dbContext.Dispatchers
                .Where(d => query.IsWorking == null || d.IsWorking == query.IsWorking)
                .Join(
                    _dbContext.Users
                        .Where(u => u.Role.Name == "Dispatcher"),
                    dispatcher => dispatcher.Id,
                    user => user.AccountId,
                    (dispatcher, user) => new { Dispatcher = dispatcher, User = user })
                .Select(x => new DispatcherDTO
                {
                    Id = x.Dispatcher.Id,
                    Email = x.User.Email,
                    FirstName = x.Dispatcher.PersonalInformation.FirstName,
                    LastName = x.Dispatcher.PersonalInformation.LastName,
                    StreetAddress = x.Dispatcher.PersonalInformation.StreetAddress,
                    HouseNumber = x.Dispatcher.PersonalInformation.HouseNumber,
                    ApartmentNumber = x.Dispatcher.PersonalInformation.ApartmentNumber,
                    City = x.Dispatcher.PersonalInformation.City,
                    PostalCode = x.Dispatcher.PersonalInformation.PostalCode,
                    StateProvince = x.Dispatcher.PersonalInformation.StateProvince,
                    Country = x.Dispatcher.PersonalInformation.Country,
                    PhoneNumber = x.Dispatcher.PersonalInformation.PhoneNumber,
                    BankAccountNumber = x.Dispatcher.BankAccountNumber,
                    IsWorking = x.Dispatcher.IsWorking,
                    Salary = x.Dispatcher.Salaries
                        .OrderByDescending(r => r.Date)
                        .First()
                        .Income
                })
                .ToArrayAsync(cancellationToken);

            if (dispatcherDTOs is null)
                return Errors.Errors.MappingError;

            return new GetAllDispatchersResponse(dispatcherDTOs);
        }
    }
}