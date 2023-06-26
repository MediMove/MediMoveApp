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
    public class GetEmployeesInMonthByHoursAndSalaryHandler : IRequestHandler<GetEmployeesInMonthByHoursAndSalaryQuery, ErrorOr<GetEmployeesInMonthByHoursAndSalaryDTO>>
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
            //var dispatchers = await _dbContext.Dispatchers
            ////.Include(p => p.PersonalInformation)


            //.Include(d => d.PersonalInformation)
            //.Join(_dbContext.Salaries,
            //dispatcher => dispatcher.Id,
            //salary => salary.DispatcherId,
            //(dispatcher, salary) => new { Id = dispatcher.Id, PersonalInformation = dispatcher.PersonalInformation, Dispatcher = dispatcher, Salary = salary })
            //.Where(d => d.Salary.Date.Date >= request.StartDate &&
            //            d.Salary.Date.Date <= request.EndDate &&
            //            d.Salary.Income / 160 >= request.StartAmount &&
            //            d.Salary.Income / 160 <= request.EndAmount)
            //.Select(x => new GetEmployeesInMonthByHoursAndSalaryDTO.GetEmployeesInMonthByHoursAndSalaryRow
            //{
            //    Id = x.Id,
            //    FirstName = x.Dispatcher.PersonalInformation.FirstName,
            //    LastName = x.Dispatcher.PersonalInformation.LastName,
            //    PhoneNumber = x.Dispatcher.PersonalInformation.PhoneNumber,
            //    SalarySum = x.Salary.Income
            //}).ToArrayAsync(cancellationToken);

            //var teamsWithParamedic = await _dbContext.Teams
            //    //.Join(_dbContext.Paramedics,
            //    //teams => teams.ParamedicId,
            //    //paramedic => paramedic.Id,
            //    //(teams, paramedic) => new { Team = teams, Paramedic = paramedic })
            //    //.Join(_dbContext.Paramedics,
            //    //teams => teams.Team.DriverId,
            //    //paramedics => paramedics.Id,
            //    //(teams, paramedics) => new { Id = teams.Team.Id, Team = teams.Team, Paramedic = teams.Paramedic, Driver = paramedics })
            //    //.Where(t => t.Team.Day.Date.Date >= request.StartDate &&
            //    //            t.Team.Day.Date.Date <= request.EndDate)
            //    //.Join(_dbContext.Rates)
            //    //teams => teams.Paramedic.Id,
            //    //rates => rates.ParamedicId,
            //    //(teams, rates) => new { Id = teams.Team.Id, Team = teams.Team, Paramedic = teams.Paramedic, Driver = teams.Driver })
            //    .Include(p => p.Paramedic)
            //        .ThenInclude(r => r.Rates)
            //    .Include(d => d.Driver)
            //        .ThenInclude(r => r.Rates)
            //    .Where(t => t.Day.Date >= request.StartDate.Date &&
            //                t.Day.Date <= request.EndDate.Date)
            //    .Select(x => new
            //    {
            //        Id = x.Id,
            //        Day = x.Day,
            //        Driver = x.Driver,
            //        Paramedic = x.Paramedic,
            //        RateP = x.Paramedic.Rates.Where(r => r.Date.Date <= x.Day.Date).OrderByDescending(r => r.Date).First().PayPerHour,
            //        RateD = x.Driver.Rates.Where(r => r.Date.Date <= x.Day.Date).OrderByDescending(r => r.Date).First().PayPerHour
            //    }).ToArrayAsync(cancellationToken);

            var teamsWithParamedic = await _dbContext.Teams
            .Include(p => p.Paramedic)
                .ThenInclude(r => r.Rates)
            .Include(d => d.Driver)
                .ThenInclude(r => r.Rates)
            .Where(t => t.Day.Date >= request.StartDate.Date &&
                        t.Day.Date <= request.EndDate.Date &&
                        (t.Paramedic.Rates.Any(r => r.Date.Date <= t.Day.Date && r.PayPerHour >= request.StartAmount && r.PayPerHour <= request.EndAmount) ||
                        t.Driver.Rates.Any(r => r.Date.Date <= t.Day.Date && r.PayPerHour >= request.StartAmount && r.PayPerHour <= request.EndAmount)))
            .Select(x => new
            {
                Id = x.Id,
                Day = x.Day,
                Driver = x.Driver,
                Paramedic = x.Paramedic,
                RateP = x.Paramedic.Rates.Where(r => r.Date.Date <= x.Day.Date).OrderByDescending(r => r.Date).First().PayPerHour,
                RateD = x.Driver.Rates.Where(r => r.Date.Date <= x.Day.Date).OrderByDescending(r => r.Date).First().PayPerHour
            })
            .ToArrayAsync(cancellationToken);


            var employeeSums = teamsWithParamedic
                .SelectMany(t => new[] { t.Driver.Id, t.Paramedic.Id })
                .Distinct()
                //.Where(t => t.)
                .ToDictionary(key => key, value => Tuple.Create((decimal)0, (decimal)0));



            foreach (var team in teamsWithParamedic)
            {
                employeeSums[team.Driver.Id] = Tuple.Create(employeeSums[team.Driver.Id].Item1 + team.RateD,
                                                            employeeSums[team.Driver.Id].Item2 + 1);
                employeeSums[team.Paramedic.Id] = Tuple.Create(employeeSums[team.Paramedic.Id].Item1 + team.RateP,
                                                                employeeSums[team.Paramedic.Id].Item2 + 1);
            }

             var paramedics = employeeSums
                .Where(x => x.Value.Item2 >= request.StartHours &&
                            x.Value.Item2 <= request.EndHours &&
                            _dbContext.Rates.Where(p => p.Id == x.Key)
                                            .Any(r => r.PayPerHour >= request.StartAmount &&
                                                      r.PayPerHour <= request.EndAmount))
                .Select(kv => new GetEmployeesInMonthByHoursAndSalaryDTO.GetEmployeesInMonthByHoursAndSalaryRow
                {
                    Id = kv.Key,
                    FirstName = _dbContext.Paramedics.Include(p => p.PersonalInformation).FirstOrDefault(e => e.Id == kv.Key)?.PersonalInformation.FirstName,
                    LastName = _dbContext.Paramedics.Include(p => p.PersonalInformation).FirstOrDefault(e => e.Id == kv.Key)?.PersonalInformation.LastName,
                    PhoneNumber = _dbContext.Paramedics.Include(p => p.PersonalInformation).FirstOrDefault(e => e.Id == kv.Key)?.PersonalInformation.PhoneNumber,
                    SalarySum = kv.Value.Item1 * 8
                })
                .OrderBy(x => x.Id)
                .ToArray();

             var result = new GetEmployeesInMonthByHoursAndSalaryDTO
             {
                 Rows = paramedics
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


    

