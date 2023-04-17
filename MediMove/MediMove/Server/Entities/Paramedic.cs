using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Entities
{
    public class Paramedic
    {
        public int Id { get; set; }
        public int PersonalInfoId { get; set; }
        public string BankAccountNumber { get; set; }
        public bool IsDriver { get; set; }
        public string PhoneNumber { get; set; }

    }
}
