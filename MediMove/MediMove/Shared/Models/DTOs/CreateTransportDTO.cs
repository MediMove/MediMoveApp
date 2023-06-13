using MediMove.Shared.Models.Enums;


namespace MediMove.Shared.Models.DTOs
{
    public class CreateTransportDTO
    {
        public int PatientId { get; set; }
        public DateTime StartTime { get; set; }
        public Financing Financing { get; set; }
        public PatientPosition PatientPosition { get; set; }
        public string Destination { get; set; }
        public TransportType TransportType { get; set; }
        public int? TeamId { get; set; }
    }
}
