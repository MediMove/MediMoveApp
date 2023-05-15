using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Queries;

public record GetAllAvailabilitiesQuery(DateTime Day) : IRequest<ErrorOr<IEnumerable<AvailabilityDTO>>>;

