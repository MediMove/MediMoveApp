using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public int Weight { get; set; }

        public virtual ICollection<Transport> Transports { get; set; }

        public int PersonalInformationId { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }
    }
}
