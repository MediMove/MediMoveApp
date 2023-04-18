using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.DTOs
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
    public class TransportsDto
    {
        public DateTime StartTime { get; set; }
        public string PatientInfoFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientStreetAddress { get; set; }
        public string PatientHouseNumber { get; set; }
        public string PatientApartamentNumber { get; set; }
        public string PatientPostalCode { get; set; }
        public PatientPosition PatientPosition { get; set; }
        public string Destination { get; set; }
        public TransportType TransportType { get; set; }

    }
}
