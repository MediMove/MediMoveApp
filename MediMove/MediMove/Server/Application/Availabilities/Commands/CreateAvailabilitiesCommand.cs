﻿using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Availabilities.Commands;

/// <summary>
/// Command for creating availabilities.
/// </summary>
/// <param name="ParamedicId">ID of paramedic as integer</param>
/// <param name="Request">CreateAvailabilitiesRequest</param>
public record CreateAvailabilitiesCommand(int ParamedicId, CreateAvailabilitiesRequest Request) : IRequest<ErrorOr<Availability[]>>;
    