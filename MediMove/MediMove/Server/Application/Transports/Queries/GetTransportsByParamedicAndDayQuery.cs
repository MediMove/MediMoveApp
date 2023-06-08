using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Queries;

public record GetTransportsByParamedicAndDayQuery(int ParamedicId, DateTime Day) : IRequest<ErrorOr<GetTransportsResponse>>;

