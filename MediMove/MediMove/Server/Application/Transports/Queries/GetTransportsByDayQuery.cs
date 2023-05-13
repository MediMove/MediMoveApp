using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs.V2;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Queries;

public record GetTransportsByDayQuery(DateTime Day) : IRequest<ErrorOr<IEnumerable<TransportDTO>>>;

