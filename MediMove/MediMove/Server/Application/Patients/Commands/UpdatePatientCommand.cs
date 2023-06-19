using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Commands
{
    public record UpdatePatientCommand(CreatePatientRequest Dto, int Id) : IRequest<ErrorOr<Patient>>;
}
