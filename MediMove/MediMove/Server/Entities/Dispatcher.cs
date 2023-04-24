using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediMove.Server.Entities;

namespace MediMove.Server.Entities
{
    public class Dispatcher
    {
        public int Id { get; set; }

        public string BankAccountNumber { get; set; }

        virtual public ICollection<Salary> Salaries { get; set; }

        public int PersonalInformationId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
    }
}
