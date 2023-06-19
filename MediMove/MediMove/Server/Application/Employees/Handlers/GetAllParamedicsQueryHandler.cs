using ErrorOr;
using MediatR;
using MediMove.Server.Application.Employees.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Employees.Handlers
{
    /// <summary>
    /// Handler for getting all paramedics.
    /// </summary>
    public class GetAllParamedicsQueryHandler : IRequestHandler<GetAllParamedicsQuery, ErrorOr<GetAllParamedicsResponse>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetAllParamedicsQueryHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public GetAllParamedicsQueryHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling GetAllParamedicsQuery.
        /// </summary>
        /// <param name="query">GetAllParamedicsQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetAllParamedicsResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetAllParamedicsResponse>> Handle(GetAllParamedicsQuery query, CancellationToken cancellationToken)
        {
            var paramedicDTOs = await _dbContext.Paramedics
                .Where(p => query.IsWorking == null || p.IsWorking == query.IsWorking)
                .Join(
                    _dbContext.Users
                        .Where(u => u.Role.Name == "Paramedic"),
                    paramedic => paramedic.Id,
                    user => user.AccountId,
                    (paramedic, user) => new { Paramedic = paramedic, User = user })
                .Select(x => new ParamedicDTO
                {
                    Id = x.Paramedic.Id,
                    Email = x.User.Email,
                    FirstName = x.Paramedic.PersonalInformation.FirstName,
                    LastName = x.Paramedic.PersonalInformation.LastName,
                    StreetAddress = x.Paramedic.PersonalInformation.StreetAddress,
                    HouseNumber = x.Paramedic.PersonalInformation.HouseNumber,
                    ApartmentNumber = x.Paramedic.PersonalInformation.ApartmentNumber,
                    City = x.Paramedic.PersonalInformation.City,
                    PostalCode = x.Paramedic.PersonalInformation.PostalCode,
                    StateProvince = x.Paramedic.PersonalInformation.StateProvince,
                    Country = x.Paramedic.PersonalInformation.Country,
                    PhoneNumber = x.Paramedic.PersonalInformation.PhoneNumber,
                    BankAccountNumber = x.Paramedic.BankAccountNumber,
                    IsWorking = x.Paramedic.IsWorking,
                    IsDriver = x.Paramedic.IsDriver,
                    PayPerHour = x.Paramedic.Rates
                        .OrderByDescending(r => r.Date)
                        .First()
                        .PayPerHour
                })
                .ToArrayAsync(cancellationToken);

            if (paramedicDTOs is null)
                return Errors.Errors.MappingError;

            return new GetAllParamedicsResponse(paramedicDTOs);
        }
    }
}