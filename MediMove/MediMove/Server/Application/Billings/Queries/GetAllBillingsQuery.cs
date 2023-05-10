using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Billings.Queries.GetAllBillingsQuery;
/*
public record GetAllBillingsDTO : IRequest<ErrorOr<IEnumerable<BillingDTO>>>;
public class GetAllBillingsQueryHandler : IRequestHandler<GetAllBillingsDTO, ErrorOr<IEnumerable<BillingDTO>>>
{
    private readonly IMapper _mapper;
    private readonly IBillingRepository _billingRepository;

    public GetAllBillingsQueryHandler(IMapper mapper, IBillingRepository billingRepository)
    {
        _mapper = mapper;
        _billingRepository = billingRepository;
    }

    public async Task<ErrorOr<IEnumerable<BillingDTO>>> Handle(GetAllBillingsDTO request, CancellationToken cancellationToken)
    {
        var billings = await _billingRepository.GetBillings();
        var billingDTOs = _mapper.Map<IEnumerable<BillingDTO>>(billings);

        if (billingDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(billingDTOs);
    }
}
*/