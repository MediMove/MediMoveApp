using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Commands;

public record CreatePatientCommand(CreatePatientDTO Dto) : IRequest<ErrorOr<int>>;
