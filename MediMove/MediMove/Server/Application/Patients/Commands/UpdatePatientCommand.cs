using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Commands
{
    public record UpdatePatientCommand(CreatePatientDTO Dto, int Id) : IRequest<ErrorOr<int>>;
}
