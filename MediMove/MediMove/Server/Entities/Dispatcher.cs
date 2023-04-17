using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Entities
{
    public class Dispatcher
    {
        public int Id { get; set; }
        public int PersonalInfoId { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
