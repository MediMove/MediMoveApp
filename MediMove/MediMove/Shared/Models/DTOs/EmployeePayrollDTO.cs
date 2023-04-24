using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class EmployeePayrollDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal Salary { get; set; }
    }
}
