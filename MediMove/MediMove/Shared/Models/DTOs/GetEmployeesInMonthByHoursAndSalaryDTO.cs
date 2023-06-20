using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{

    public class GetEmployeesInMonthByHoursAndSalaryDTO
    {
        public class GetEmployeesInMonthByHoursAndSalaryRow
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public decimal SalarySum { get; set; }
        }
        public GetEmployeesInMonthByHoursAndSalaryRow[] Rows { get; set; }

    }

}
