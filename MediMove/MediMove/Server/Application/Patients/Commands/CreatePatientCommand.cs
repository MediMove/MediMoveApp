using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Commands;

public record CreatePatientCommand(CreatePatientRequest Request) : IRequest<ErrorOr<Patient>>;
