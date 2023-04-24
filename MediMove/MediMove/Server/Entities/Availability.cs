using MediMove.Shared.Entities;

namespace MediMove.Server.Entities
{
    public enum ShiftType
    {
        Morning,
        Evening
    }
    public class Availability
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public ShiftType ShiftType { get; set; }

        public int ParamedicId { get; set; }
        public Paramedic Paramedic { get; set; }
    }
}
