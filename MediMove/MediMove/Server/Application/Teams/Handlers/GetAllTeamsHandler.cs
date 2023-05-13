using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Teams.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Teams.Handlers
{
    public class GetAllTeamsHandler : IRequestHandler<GetAllTeamsQuery, ErrorOr<IEnumerable<TeamDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetAllTeamsHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<IEnumerable<TeamDTO>>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = await _dbContext.Teams
                .Where( d => d.Day.Date == request.Day.Date)
                .ToListAsync(cancellationToken);

            var teamDTOs = _mapper.Map<IEnumerable<TeamDTO>>(teams);

            if (teamDTOs is null)
                return Errors.Errors.MappingError;

            return ErrorOr.ErrorOr.From(teamDTOs);
        }
    }

}
