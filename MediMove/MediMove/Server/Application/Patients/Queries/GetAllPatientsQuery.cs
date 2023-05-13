using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs.V2;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Patients.Queries;

public record GetAllPatientsQuery : IRequest<ErrorOr<IEnumerable<PatientDTO>>>;

