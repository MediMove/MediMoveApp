using ErrorOr;
using MediatR;
using MediMove.Shared.Models.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class CreateAvailabilitiesDTO
    {
        public IEnumerable<DateTime> Days { get; set; }
        public IEnumerable<ShiftType> ShiftTypes { get; set; }
        public int ParamedicId { get; set; }
    }
}
