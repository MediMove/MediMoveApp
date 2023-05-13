using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs.V2;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Commands;

public record CreateAvailabilitiesCommand(CreateAvailabilitiesDTO Dto) : IRequest<ErrorOr<int>>;
    