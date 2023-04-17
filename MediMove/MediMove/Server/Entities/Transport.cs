using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Entities
{
    public enum PatientPosition
    {
        Walking,
        Sitting,
        Lying

    }

    public enum Financing
    {
        FullyFunded,
        PartiallyFunded,
        FullyPaid
    }

    public enum TransportType
    {
        Visit,
        Handover
    }

    public class Transport
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int PatientId { get; set; }
        public int? BillingId { get; set; }
        public PatientPosition PatientPosition { get; set; }
        public Financing Financing { get; set; }
        public DateTime StartTime { get; set; }
        public string Destination { get; set; }

        public virtual Team Team { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Billing Billing { get; set; }

    }
}
