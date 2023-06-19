using ErrorOr;
using MediatR;
using MediMove.Server.Application.Employees.Queries;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Employees.Handlers
{
    /// <summary>
    /// Handler for getting all employees.
    /// </summary>
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ErrorOr<GetAllEmployeesResponse>>
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor for GetAllEmployeesQueryHandler.
        /// </summary>
        /// <param name="mediator">mediator to inject</param>
        public GetAllEmployeesQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method for handling GetAllEmployeesQuery.
        /// </summary>
        /// <param name="query">GetAllEmployeesQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetAllEmployeesResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetAllEmployeesResponse>> Handle(GetAllEmployeesQuery query, CancellationToken cancellationToken)
        {
            var paramedics = await _mediator.Send(new GetAllParamedicsQuery(query.IsWorking), cancellationToken);
            if (paramedics.IsError)
                return paramedics.Errors;

            var dispatchers = await _mediator.Send(new GetAllDispatchersQuery(query.IsWorking), cancellationToken);
            if (dispatchers.IsError)
                return dispatchers.Errors;

            return new GetAllEmployeesResponse(
                paramedics.Value.Paramedics,
                dispatchers.Value.Dispatchers
            );
        }
    }
}