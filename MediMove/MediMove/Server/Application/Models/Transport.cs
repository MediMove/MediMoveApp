using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Application.Models
{
    public class Transport
    {
        public int Id { get; set; }
        public PatientPosition PatientPosition { get; set; }
        public Financing Financing { get; set; }
        public DateTime StartTime { get; set; }
        public string Destination { get; set; }
        public TransportType TransportType { get; set; }
        public bool IsCancelled { get; set; } = false;

        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public int? BillingId { get; set; }
        public virtual Billing? Billing { get; set; }
    }
}
