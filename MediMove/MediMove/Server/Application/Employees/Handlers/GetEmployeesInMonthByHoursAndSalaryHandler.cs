using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Employees.Queries;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Employees.Handlers
{
    public class GetEmployeesInMonthByHoursAndSalaryHandler: IRequestHandler<GetEmployeesInMonthByHoursAndSalaryQuery, ErrorOr<GetEmployeesInMonthByHoursAndSalaryDTO>>
    {
       
            private TProperty GetValueOrDefault<TModel, TProperty>(TModel model, string propertyName)
            {
                var propertyInfo = typeof(TModel).GetProperty(propertyName);
                if (propertyInfo != null && propertyInfo.PropertyType == typeof(TProperty))
                {
                    return (TProperty)propertyInfo.GetValue(model);
                }

                return default(TProperty);
            }
            private readonly IMapper _mapper;
            private readonly MediMoveDbContext _dbContext;

            public GetEmployeesInMonthByHoursAndSalaryHandler(IMapper mapper, MediMoveDbContext dbContext)
            {
                _mapper = mapper;
                _dbContext = dbContext;
            }

        public async Task<ErrorOr<GetEmployeesInMonthByHoursAndSalaryDTO>> Handle(GetEmployeesInMonthByHoursAndSalaryQuery request, CancellationToken cancellationToken)
        {
            var dispatchers = await _dbContext.Dispatchers
            //.Include(p => p.PersonalInformation)


            .Include(d => d.PersonalInformation)
            .Join(_dbContext.Salaries,
            dispatcher => dispatcher.Id,
            salary => salary.DispatcherId,
            (dispatcher, salary) => new { Id = dispatcher.Id, PersonalInformation = dispatcher.PersonalInformation, Dispatcher = dispatcher, Salary = salary })
            .Where(d => d.Salary.Date.Date >= request.StartDate &&
                        d.Salary.Date.Date <= request.EndDate &&
                        d.Salary.Income / 160 >= request.StartAmount &&
                        d.Salary.Income / 160 <= request.EndAmount)
            .Select(x => new GetEmployeesInMonthByHoursAndSalaryDTO.GetEmployeesInMonthByHoursAndSalaryRow
            {
                Id = x.Id,
                FirstName = x.Dispatcher.PersonalInformation.FirstName,
                LastName = x.Dispatcher.PersonalInformation.LastName,
                PhoneNumber = x.Dispatcher.PersonalInformation.PhoneNumber,
                SalarySum = x.Salary.Income
            }).ToArrayAsync(cancellationToken);

            var teamsWithParamedic = await _dbContext.Teams
                //.Join(_dbContext.Paramedics,
                //teams => teams.ParamedicId,
                //paramedic => paramedic.Id,
                //(teams, paramedic) => new { Team = teams, Paramedic = paramedic })
                //.Join(_dbContext.Paramedics,
                //teams => teams.Team.DriverId,
                //paramedics => paramedics.Id,
                //(teams, paramedics) => new { Id = teams.Team.Id, Team = teams.Team, Paramedic = teams.Paramedic, Driver = paramedics })
                //.Where(t => t.Team.Day.Date.Date >= request.StartDate &&
                //            t.Team.Day.Date.Date <= request.EndDate)
                //.Join(_dbContext.Rates)
                //teams => teams.Paramedic.Id,
                //rates => rates.ParamedicId,
                //(teams, rates) => new { Id = teams.Team.Id, Team = teams.Team, Paramedic = teams.Paramedic, Driver = teams.Driver })
                .Include(p => p.Paramedic)
                    .ThenInclude(r => r.Rates)
                .Include(d => d.Driver)
                    .ThenInclude(r => r.Rates)
                .Where(t => t.Day.Date >= request.StartDate.Date &&
                            t.Day.Date <= request.EndDate.Date)
                .Select(x => new
                {
                    Id = x.Id,
                    Day = x.Day,
                    Driver = x.Driver,
                    Paramedic = x.Paramedic,
                    RateP = x.Paramedic.Rates.Where(r => r.Date.Date <= x.Day.Date).OrderByDescending(r => r.Date).First().PayPerHour,
                    RateD = x.Driver.Rates.Where(r => r.Date.Date <= x.Day.Date).OrderByDescending(r => r.Date).First().PayPerHour
                }).ToArrayAsync(cancellationToken);

        var employeeSums = teamsWithParamedic
            .SelectMany(t => new[] { t.Driver.Id, t.Paramedic.Id })
            .Distinct()
            .ToDictionary(key => key, value => (decimal)0);



            foreach (var team in teamsWithParamedic)
            {
                employeeSums[team.Driver.Id] = employeeSums[team.Driver.Id] + team.RateD;
                employeeSums[team.Paramedic.Id] = employeeSums[team.Paramedic.Id] + team.RateP;
            }

            var paramedics = employeeSums.Select(kv => new GetEmployeesInMonthByHoursAndSalaryDTO.GetEmployeesInMonthByHoursAndSalaryRow
            {
                Id = kv.Key,
                FirstName = _dbContext.Paramedics.Include(p => p.PersonalInformation).FirstOrDefault(e => e.Id == kv.Key)?.PersonalInformation.FirstName,
                LastName = _dbContext.Paramedics.Include(p => p.PersonalInformation).FirstOrDefault(e => e.Id == kv.Key)?.PersonalInformation.LastName,
                PhoneNumber = _dbContext.Paramedics.Include(p => p.PersonalInformation).FirstOrDefault(e => e.Id == kv.Key)?.PersonalInformation.PhoneNumber,
                SalarySum = kv.Value * 80
            }).ToArray();



            //var paramedics = await _dbContext.Paramedics
            ////.Include(p => p.PersonalInformation)
            //.Include(d => d.PersonalInformation)
            //.Join(_dbContext.Rates,
            //paramedic => paramedic.Id,
            //rate => rate.ParamedicId,
            //(paramedic, rate) => new { Id = paramedic.Id, PersonalInformation = paramedic.PersonalInformation, Paramedic = paramedic, Rate = rate })
            //.Where(d => d.Rate.Date.Date >= request.StartDate &&
            //            d.Rate.Date.Date <= request.EndDate &&
            //            d.Rate.PayPerHour >= request.StartAmount &&
            //            d.Rate.PayPerHour <= request.EndAmount)
            //.Select(x => new GetEmployeesInMonthByHoursAndSalaryDTO.GetEmployeesInMonthByHoursAndSalaryRow
            //{
            //    Id = x.Id,
            //    FirstName = x.Paramedic.PersonalInformation.FirstName,
            //    LastName = x.Paramedic.PersonalInformation.LastName,
            //    PhoneNumber = x.Paramedic.PersonalInformation.PhoneNumber,
            //    SalarySum = x.Rate.PayPerHour
            //}).ToArrayAsync(cancellationToken);
            //.GroupBy(joinResult => joinResult.PerosnalInfo)
            //.Select(patient => new GetPatientsByDateAndPaymentsSumDTO.GetPatientsByDateAndPaymentsSumRow
            //{
            //    Id = patient.Key.Id,
            //    Weight = patient.FirstOrDefault().Weight,
            //    FirstName = patient.Key.FirstName,
            //    LastName = patient.Key.LastName,
            //    StreetAddress = patient.Key.StreetAddress,
            //    HouseNumber = patient.Key.HouseNumber,
            //    ApartmentNumber = patient.Key.ApartmentNumber,
            //    City = patient.Key.City,
            //    PostalCode = patient.Key.PostalCode,
            //    StateProvince = patient.Key.StateProvince,
            //    Country = patient.Key.Country,
            //    PhoneNumber = patient.Key.PhoneNumber,
            //    PaymentsSum = patient.Sum(t => t.Billing.Cost)
            //})
            //.Where(dto => dto.PaymentsSum >= request.StartAmount &&
            //              dto.PaymentsSum <= request.EndAmount)
            //.ToListAsync(cancellationToken);

            //var filteredPatients;
            //foreach (var patient in patients)
            //{
            //    filteredPatients += patient.Sum(p => p.Billing.Cost).To
            //}
            //DateTime InvoiceDate = DateTime.UtcNow;

            //var temp = InvoiceDate.Date >= StartDate.Date && InvoiceDate.Date <= EndDate.Date;
            var result = new GetEmployeesInMonthByHoursAndSalaryDTO
            {
                Rows = dispatchers.Concat(paramedics).ToArray()
            };

            var GetEmployeesInMonthByHoursAndSalaryDTO = _mapper.Map<GetEmployeesInMonthByHoursAndSalaryDTO>(result);


            if (GetEmployeesInMonthByHoursAndSalaryDTO is null)
                return Errors.Errors.MappingError;

            return ErrorOr.ErrorOr.From(GetEmployeesInMonthByHoursAndSalaryDTO);

            //return ErrorOr<GetPatientsByDateAndPaymentsSumDTO>.Success(result);
            //var paramedics = await _dbContext.Paramedics
            //     .Include(e => e.Transports)
            //     .Include(e => e.Rates)
            //     .ToListAsync(cancellationToken);
            //throw new NotImplementedException();
        }
        }
    }

