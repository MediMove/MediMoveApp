using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MediMove.Server.Application.Transports.Handlers
{
    public class GetTransportsByTeamAndDayQueryHandler : IRequestHandler<GetTransportsByTeamAndDayQuery, ErrorOr<GetTransportsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetTransportsByTeamAndDayQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<GetTransportsResponse>> Handle(GetTransportsByTeamAndDayQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Transports
                .Where(t => 
                    t.StartTime.Date == request.Day.Date &&
                    t.Team.Id == request.TeamId &&
                    !t.IsCancelled
                    )
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .Select(t => new
                {
                    t.Id,
                    GetTransportResponse = _mapper.Map<GetTransportResponse>(t)
                })
                .ToDictionaryAsync(
                    keySelector: t => t.Id,
                    elementSelector: t => t.GetTransportResponse, 
                    cancellationToken
                );


            if (query is null)
                return Errors.Errors.MappingError;

            var response = new GetTransportsResponse
            {
                Transports = query
            };

            return ErrorOr.ErrorOr.From(response);
        }
    }
}
