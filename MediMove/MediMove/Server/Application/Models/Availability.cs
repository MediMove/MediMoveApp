using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Application.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public ShiftType? ShiftType { get; set; } // if null, paramedic is available for all shifts on a day
        public int ParamedicId { get; set; }
        public virtual Paramedic Paramedic { get; set; }
    }
}