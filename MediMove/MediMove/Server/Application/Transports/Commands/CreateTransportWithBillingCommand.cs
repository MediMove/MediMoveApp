﻿using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Commands;

public record CreateTransportWithBillingCommand(CreateTransportWithBillingDTO Dto) : IRequest<ErrorOr<Transport>>;