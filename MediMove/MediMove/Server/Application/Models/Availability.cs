using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Application.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public ShiftType ShiftType { get; set; }
        public int ParamedicId { get; set; }
        public virtual Paramedic Paramedic { get; set; }
    }
}
