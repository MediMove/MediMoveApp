﻿using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Authentication.Commands;

/// <summary>
/// Command for registering a paramedic.
/// </summary>
/// <param name="Request"><see cref="RegisterParamedicRequest"/></param>
public record RegisterParamedicCommand(RegisterParamedicRequest Request) : IRequest<ErrorOr<Paramedic>>;