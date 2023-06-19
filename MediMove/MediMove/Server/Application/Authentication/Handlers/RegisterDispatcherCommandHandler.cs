using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;
namespace MediMove.Server.Application.Authentication.Handlers;

/// <summary>
/// Handler for registering a new dispatcher.
/// </summary>
public class RegisterDispatcherCommandHandler : IRequestHandler<RegisterDispatcherCommand, ErrorOr<Dispatcher>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor for <see cref="RegisterDispatcherCommandHandler"/>.
    /// </summary>
    /// <param name="mapper">mapper to inject</param>
    /// <param name="dbContext">dbContext to inject</param>
    /// <param name="mediator">mediator to inject</param>
    public RegisterDispatcherCommandHandler(IMapper mapper, MediMoveDbContext dbContext, IMediator mediator)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _mediator = mediator;
    }

    /// <summary>
    /// Method for handling <see cref="RegisterDispatcherCommand"/>.
    /// </summary>
    /// <param name="command"><see cref="RegisterDispatcherCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Dispatcher"/> wrapped in ErrorOr</returns>
    public async Task<ErrorOr<Dispatcher>> Handle(RegisterDispatcherCommand command, CancellationToken cancellationToken)
    {
        var newDispatcher = _mapper.Map<Dispatcher>(command.Request);

        if (newDispatcher is null)
            return Errors.Errors.MappingError;

        newDispatcher.PersonalInformation = _mapper.Map<PersonalInformation>(command.Request);

        if (newDispatcher.PersonalInformation is null)
            return Errors.Errors.MappingError;

        newDispatcher.Salaries = new Salary[] {
            new Salary
            {
                Date = DateTime.Now,
                Income = command.Request.Salary
            }
        };

        var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            await _dbContext.AddAsync(newDispatcher, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var newUser = await _mediator.Send(new RegisterUserCommand(command.Request.Email, command.Request.Password, newDispatcher.Id, "Dispatcher"), cancellationToken);
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

        return newDispatcher;
    }
}