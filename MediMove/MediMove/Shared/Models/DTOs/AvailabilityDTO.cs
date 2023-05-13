using ErrorOr;
using MediatR;
using MediMove.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class AvailabilityDTO
    {
        public ShiftType ShiftType { get; set; }
        public string ParamedicFirstName { get; set; }
        public string ParamedicLastName { get; set; }
    }
}
