﻿using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Commands
{
    public record UpdateTransportCommand(CreateTransportDTO Dto, int Id) : IRequest<ErrorOr<int>>;
}
