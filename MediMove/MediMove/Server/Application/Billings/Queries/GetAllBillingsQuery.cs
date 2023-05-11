using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Billings.Queries.GetAllBillingsQuery;
/*
public record GetAllBillingsDTO : IRequest<ErrorOr<IEnumerable<BillingDTO>>>;
public class GetAllBillingsQueryHandler : IRequestHandler<GetAllBillingsDTO, ErrorOr<IEnumerable<BillingDTO>>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetAllBillingsQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<IEnumerable<BillingDTO>>> Handle(GetAllBillingsDTO request, CancellationToken cancellationToken)
    {
        var billings = await _dbContext.Billings.ToListAsync(cancellationToken: cancellationToken);

        var billingDTOs = _mapper.Map<IEnumerable<BillingDTO>>(billings);

        if (billingDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(billingDTOs);
    }
}
*/