using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs.temp;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Commands.CreateAvailabilitiesCommand;

public class CreateAvailabilitiesCommandHandler : IRequestHandler<CreateAvailabilitiesDTO, ErrorOr<Unit>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public CreateAvailabilitiesCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Unit>> Handle(CreateAvailabilitiesDTO request, CancellationToken cancellationToken)
    {
        var availabilities = _mapper.Map<IEnumerable<Availability>>(request);

        if (availabilities is null)
            return Errors.Errors.MappingError;

        // TODO: Add validation

        await _dbContext.Availabilities.AddRangeAsync(availabilities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ErrorOr<Unit>();
    }
}