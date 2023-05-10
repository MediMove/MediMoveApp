using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Paramedics.Commands.CreateParamedicCommand;

public class CreateParamedicCommandHandler : IRequestHandler<CreateParamedicDTO, ErrorOr<int>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public CreateParamedicCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<int>> Handle(CreateParamedicDTO request, CancellationToken cancellationToken)
    {
        var paramedic = _mapper.Map<Paramedic>(request);

        if (paramedic is null)
            return Errors.Errors.MappingError;

        // TODO: Add validation

        await _dbContext.Paramedics.AddAsync(paramedic, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return paramedic.Id;
    }
}
