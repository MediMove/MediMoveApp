using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Queries.GetTransportsByParamedicAndDayQuery;

public record GetTransportsByParamedicAndDayDTO(int ParamedicId, DateOnly Day) : IRequest<ErrorOr<IEnumerable<TransportDTO>>>;
public class GetTransportsByParamedicAndDayQueryHandler : IRequestHandler<GetTransportsByParamedicAndDayDTO, ErrorOr<IEnumerable<TransportDTO>>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetTransportsByParamedicAndDayQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<IEnumerable<TransportDTO>>> Handle(GetTransportsByParamedicAndDayDTO request, CancellationToken cancellationToken)
    {
        var dateOnly = request.Day;
        var transports = await _dbContext.Transports
                .Where(t =>
                    dateOnly.Day == t.StartTime.Day &&
                    dateOnly.Year == t.StartTime.Year &&
                    dateOnly.Month == t.StartTime.Month)
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .ToListAsync(cancellationToken: cancellationToken);

        var transportDTOs = _mapper.Map<IEnumerable<TransportDTO>>(transports);

        if (transportDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(transportDTOs);
    }
}
