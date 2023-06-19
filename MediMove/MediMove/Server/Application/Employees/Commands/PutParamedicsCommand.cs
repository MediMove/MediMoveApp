using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Employees.Commands;

public record PutParamedicsCommand(ParamedicDTO[] Paramedics) : IRequest<ErrorOr<Paramedic[]>>;