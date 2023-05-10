using ErrorOr;
using MediatR;
using MediMove.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class CreateTransportDTO : IRequest<ErrorOr<int>>
    {
        public int PatientId { get; set; }
        public DateTime StartTime { get; set; }
        public Financing Financing { get; set; }
        public PatientPosition PatientPosition { get; set; }
        public string Destination { get; set; }
        public TransportType TransportType { get; set; }
    }
}
