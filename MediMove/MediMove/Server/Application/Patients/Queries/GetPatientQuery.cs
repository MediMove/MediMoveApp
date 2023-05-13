using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs.V2;

namespace MediMove.Server.Application.Patients.Queries;

public record GetPatientQuery(int Id) : IRequest<ErrorOr<PatientDTO>>;

