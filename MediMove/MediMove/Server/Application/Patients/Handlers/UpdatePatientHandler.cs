using ErrorOr;
using MediatR;
using MediMove.Server.Application.Patients.Commands;

namespace MediMove.Server.Application.Patients.Handlers
{
    public class UpdatePatientHandler : IRequestHandler<UpdatePatientCommand, ErrorOr<int>>
    {
        public async Task<ErrorOr<int>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
