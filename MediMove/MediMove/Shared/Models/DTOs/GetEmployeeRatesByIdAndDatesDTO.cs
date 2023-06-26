using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class GetEmployeeRatesByIdAndDatesDTO
    {
        public class GetEmployeeRatesByIdAndDatesRow
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            //public string PhoneNumber { get; set; }
            public decimal Month { get; set; }
            public decimal Year { get; set; }
            public decimal SalarySum { get; set; }
        }
        public GetEmployeeRatesByIdAndDatesRow[] Rows { get; set; }

    }
}
