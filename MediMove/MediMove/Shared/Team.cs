using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared
{
    public class Team
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int ParamedicId { get; set; }
        public DateTime Day { get; set; }
    }
}
