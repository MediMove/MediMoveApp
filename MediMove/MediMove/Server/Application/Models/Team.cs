using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Application.Models
{
    public class Team
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public ShiftType ShiftType { get; set; } // added so paramedic can be available for more than one shift on a day

        public virtual ICollection<Transport> Transports { get; set; }

        public int DriverId { get; set; }
        virtual public Paramedic Driver { get; set; }

        public int ParamedicId { get; set; }
        public virtual Paramedic Paramedic { get; set; }
    }
}