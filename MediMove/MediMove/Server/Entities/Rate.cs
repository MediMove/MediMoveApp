using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Server.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        public decimal PayPerHour { get; set; }


        public int ParamedicId { get; set; }
        public Paramedic Paramedic { get; set; }


    }
}
