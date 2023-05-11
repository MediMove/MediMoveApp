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
    public class AvailabilityDTO : IRequest<ErrorOr<int>>
    {
        public DateOnly Day { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
