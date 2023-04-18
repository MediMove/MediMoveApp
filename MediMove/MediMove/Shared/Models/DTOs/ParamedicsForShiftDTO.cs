using MediMove.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class ParamedicsForShiftDTO
    {
        public ShiftType ShiftType { get; set; }
        public DateOnly Day { get; set; }
        public List<(int id, string firstName, string lastName, bool isDriver)> Paramedics { get; set; }
    }
}
