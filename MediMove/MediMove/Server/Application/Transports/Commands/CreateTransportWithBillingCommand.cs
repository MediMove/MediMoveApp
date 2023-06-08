using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Commands;

public record CreateTransportWithBillingCommand(CreateTransportWithBillingDTO Dto) : IRequest<ErrorOr<Transport>>;