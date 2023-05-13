
using MediMove.Shared.Models.Enums;


namespace MediMove.Shared.Models.DTOs.V2
{
    public class CreateTransportDTO
    {
        public int PatientId { get; set; }
        public DateTime StartTime { get; set; }
        public Financing Financing { get; set; }
        public PatientPosition PatientPosition { get; set; }
        public string Destination { get; set; }
        public TransportType TransportType { get; set; }
    }
}
