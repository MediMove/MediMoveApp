using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediMove.Server.Entities;

namespace MediMove.Shared.Entities
{
    public class Paramedic
    {
        public int Id { get; set; }
        
        public string BankAccountNumber { get; set; }
        public bool IsDriver { get; set; }
        

        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Availability> Availabilities { get; set; }

        public int PersonalInformationId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
    }
}
