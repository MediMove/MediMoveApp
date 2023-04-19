using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs
{
    public class DisplayTransportsDTO
    {
        public string PatientInfoFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientStreetAddress { get; set; }
        public string PatientHouseNumber { get; set; }
        public string PatientApartamentNumber { get; set; }
        public string PatientPostalCode { get; set; }
        public string PatientCity { get; set; }
        public PatientPosition PatientPosition { get; set; }
        public TransportType TransportType { get; set; }
        public DateTime StartTime { get; set; }
        public string Destination { get; set; }
    }
}
