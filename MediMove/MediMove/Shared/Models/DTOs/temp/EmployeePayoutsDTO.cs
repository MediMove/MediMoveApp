using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs.temp
{
    public class EmployeePayoutsDTO
    {
        public DateOnly Month { get; set; }
        public decimal Payout { get; set; }
    }
}
