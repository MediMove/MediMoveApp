using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Paramedics.Commands.CreateParamedicCommand;

public class CreateParamedicCommandHandler : IRequestHandler<CreateParamedicDTO, ErrorOr<int>>
{
    private readonly IMapper _mapper;
    private readonly IParamedicRepository _paramedicRepository;

    public CreateParamedicCommandHandler(IMapper mapper, IParamedicRepository paramedicRepository)
    {
        _mapper = mapper;
        _paramedicRepository = paramedicRepository;
    }

    public async Task<ErrorOr<int>> Handle(CreateParamedicDTO request, CancellationToken cancellationToken)
    {
        var paramedic = _mapper.Map<Paramedic>(request);

        if (paramedic is null)
            return Errors.Errors.MappingError;

        throw new NotImplementedException("Validation not implemented");
        // TODO: Add paramedic to database

        return paramedic.Id;
    }
}
