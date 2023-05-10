﻿using ErrorOr;
using MediatR;
using MediMove.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs.temp
{
    public class CreateAvailabilitiesDTO : IRequest<ErrorOr<Unit>>
    {
        public int paramedicId { get; set; }
        IEnumerable<ShiftType> shiftTypes { get; set; }
    }
}
