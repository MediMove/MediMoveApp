using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Employees.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Employees.Handlers
{
    public class GetEmployeeRatesByIdAndDatesHandler : IRequestHandler<GetEmployeeRatesByIdAndDatesQuery, ErrorOr<GetEmployeeRatesByIdAndDatesDTO>>
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

        public GetEmployeeRatesByIdAndDatesHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<GetEmployeeRatesByIdAndDatesDTO>> Handle(GetEmployeeRatesByIdAndDatesQuery request, CancellationToken cancellationToken)
        {

            var teamsWithParamedic = await _dbContext.Teams
           .Include(p => p.Paramedic)
               .ThenInclude(r => r.Rates)
           .Include(d => d.Driver)
               .ThenInclude(r => r.Rates)
           .Where(t => (t.ParamedicId == request.id ||
                       t.DriverId == request.id) &&
                       t.Day.Date >= request.StartDate.Date &&
                       t.Day.Date <= request.EndDate.Date)
           .Select(x => new
           {
               Id = x.Id,
               Date = x.Day,
               Driver = x.Driver,
               Paramedic = x.Paramedic,
               RateP = x.Paramedic.Rates.Where(r => r.Date.Date <= x.Day.Date).OrderByDescending(r => r.Date).First().PayPerHour,
               RateD = x.Driver.Rates.Where(r => r.Date.Date <= x.Day.Date).OrderByDescending(r => r.Date).First().PayPerHour
           })
           .ToArrayAsync(cancellationToken);


            var employeeSums = teamsWithParamedic
                .SelectMany(t => new[] { Tuple.Create(t.Driver.Id, t.Date.Month, t.Date.Year), Tuple.Create(t.Paramedic.Id, t.Date.Month, t.Date.Year)})
                .Distinct()
                //.Where(t => t.Item1 == request.id)
                .ToDictionary(key => key, value => (decimal)0);



            foreach (var team in teamsWithParamedic)
            {
                
                employeeSums[Tuple.Create(team.Driver.Id, team.Date.Month, team.Date.Year)] = employeeSums[Tuple.Create(team.Driver.Id, team.Date.Month, team.Date.Year)] + team.RateD;
                
                employeeSums[Tuple.Create(team.Paramedic.Id, team.Date.Month, team.Date.Year)] = employeeSums[Tuple.Create(team.Paramedic.Id, team.Date.Month, team.Date.Year)] + team.RateP;
            }
            var paramedicRates = employeeSums
                .Where(x => x.Key.Item1 == request.id)
                .Select(kv => new GetEmployeeRatesByIdAndDatesDTO.GetEmployeeRatesByIdAndDatesRow
                {
                    Id = kv.Key.Item1,
                    FirstName = _dbContext.Paramedics.Include(p => p.PersonalInformation).FirstOrDefault(e => e.Id == kv.Key.Item1 )?.PersonalInformation.FirstName,
                    LastName = _dbContext.Paramedics.Include(p => p.PersonalInformation).FirstOrDefault(e => e.Id == kv.Key.Item1 )?.PersonalInformation.LastName,
                    Month = kv.Key.Item2,
                    Year = kv.Key.Item3,
                    SalarySum = kv.Value * 8
                })
                .OrderBy(x => x.Id)
                .ToArray();

            var result = new GetEmployeeRatesByIdAndDatesDTO
            {
                Rows = paramedicRates
            };

            var GetEmployeeRatesByIdAndDatesDTO = _mapper.Map<GetEmployeeRatesByIdAndDatesDTO>(result);


            if (GetEmployeeRatesByIdAndDatesDTO is null)
                return Errors.Errors.MappingError;

            return ErrorOr.ErrorOr.From(GetEmployeeRatesByIdAndDatesDTO);
        }
    }
}
