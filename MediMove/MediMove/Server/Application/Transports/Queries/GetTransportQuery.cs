using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Queries;

public record GetTransportQuery(
    int Id)
    : IRequest<ErrorOr<TransportDTO>>;