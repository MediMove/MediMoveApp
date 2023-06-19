using ErrorOr;
using MediatR;
using MediMove.Server.Application.Employees.Commands;

namespace MediMove.Server.Application.Employees.Handlers
{
    public class PutEmployeesCommandHandler : IRequestHandler<PutEmployeesCommand, ErrorOr<Unit>>
    {
        private readonly IMediator _mediator;

        public PutEmployeesCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ErrorOr<Unit>> Handle(PutEmployeesCommand command, CancellationToken cancellationToken)
        {
            var paramedics = await _mediator.Send(new PutParamedicsCommand(command.Request.Paramedics), cancellationToken);
            if (paramedics.IsError)
                return paramedics.Errors;

            var dispatchers = await _mediator.Send(new PutDispatchersCommand(command.Request.Dispatchers), cancellationToken);
            if (dispatchers.IsError)
                return dispatchers.Errors;

            return new ErrorOr<Unit>();
        }
    }
}