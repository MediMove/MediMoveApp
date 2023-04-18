using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Server.Entities
{
    public class Team
    {

        public int Id { get; set; }
        public DateTime Day { get; set; }

        virtual public ICollection<Transport> Transports { get; set; }

        virtual public int DriverId { get; set; }
        virtual public Paramedic Driver { get; set; }

        virtual public int ParamedicId { get; set; }
        virtual public Paramedic Paramedic { get; set; }
    }
}
