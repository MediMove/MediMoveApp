using MediMove.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class CreateTeamDTO
    {
        public (int, int) Paramedics { get; set; }
        public DateOnly Day { get; set; }
    }
}
