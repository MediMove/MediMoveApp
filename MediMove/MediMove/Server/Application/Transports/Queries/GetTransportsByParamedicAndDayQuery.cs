using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Queries.GetTransportsByParamedicAndDayQuery;

public class GetTransportsByParamedicAndDayQueryHandler : IRequestHandler<GetTransportsByParamedicAndDayDTO, ErrorOr<IEnumerable<TransportDTO>>>
{
    private readonly IMapper _mapper;
    private readonly ITransportRepository _transportRepository;

    public GetTransportsByParamedicAndDayQueryHandler(IMapper mapper, ITransportRepository transportRepository)
    {
        _mapper = mapper;
        _transportRepository = transportRepository;
    }

    public async Task<ErrorOr<IEnumerable<TransportDTO>>> Handle(GetTransportsByParamedicAndDayDTO request, CancellationToken cancellationToken)
    {
        var transports = await _transportRepository.GetByParamedicAndDay(request.ParamedicId, request.Day.ToDateOnly());

        var transportDTOs = _mapper.Map< IEnumerable<TransportDTO>>(transports);

        if (transportDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(transportDTOs);
    }
}
