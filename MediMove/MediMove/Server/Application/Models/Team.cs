using System.ComponentModel.DataAnnotations.Schema;

namespace MediMove.Server.Application.Models
{
    public class Team
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public virtual ICollection<Transport> Transports { get; set; }

        public int? DriverId { get; set; }
        virtual public Paramedic Driver { get; set; }

        public int? ParamedicId { get; set; }
        public virtual Paramedic Paramedic { get; set; }
    }
}
