using MediMove.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class TransportDTO
    {
        public int TeamId { get; set; }
        public int PatientId { get; set; }
        public int BillingId { get; set; }
        public PatientPosition PatientPosition { get; set; }
        public Financing Financing { get; set; }
        public TransportType TransportType { get; set; }
        public DateTime StartTime { get; set; }
        public string Destination { get; set; }
    }
}
