using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;

namespace MediMove.Server.Application.Authentication.Handlers;

/// <summary>
/// Command for registering a new paramedic.
/// </summary>
public class RegisterParamedicCommandHandler : IRequestHandler<RegisterParamedicCommand, ErrorOr<Paramedic>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor for <see cref="RegisterParamedicCommandHandler"/>.
    /// </summary>
    /// <param name="mapper">mapper to inject</param>
    /// <param name="dbContext">dbContext to inject</param>
    /// <param name="mediator">mediator to inject</param>
    public RegisterParamedicCommandHandler(IMapper mapper, MediMoveDbContext dbContext, IMediator mediator)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _mediator = mediator;
    }

    /// <summary>
    /// Method for handling <see cref="RegisterParamedicCommand"/>.
    /// </summary>
    /// <param name="command"><see cref="RegisterParamedicCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Paramedic"/> wrapped in ErrorOr</returns>
    public async Task<ErrorOr<Paramedic>> Handle(RegisterParamedicCommand command, CancellationToken cancellationToken)
    {
        var newParamedic = _mapper.Map<Paramedic>(command.Request);

        if (newParamedic is null)
            return Errors.Errors.MappingError;

        newParamedic.PersonalInformation = _mapper.Map<PersonalInformation>(command.Request);

        if (newParamedic.PersonalInformation is null)
            return Errors.Errors.MappingError;

        newParamedic.Rates = new Rate[] {
            new Rate
            {
                Date = DateTime.Now,
                PayPerHour = command.Request.PayPerHour
            }
        };

        var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            await _dbContext.AddAsync(newParamedic, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var newUser = await _mediator.Send(new RegisterUserCommand(command.Request.Email, command.Request.Password, newParamedic.Id, "Paramedic"), cancellationToken);
            if (newUser.IsError)
            {
                transaction.Rollback();
                return newUser.Errors;
            }
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }

        transaction.Commit();

        return newParamedic;
    }
}