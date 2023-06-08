using MediMove.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MediMove.Shared.Models.DTOs
{
    public class GetTransportResponse
    {

        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientStreetAddress { get; set; }
        public string PatientHouseNumber { get; set; }
        public int PatientApartmentNumber { get; set; }
        public string PatientPostalCode { get; set; }
        public string PatientCity { get; set; }
        public int PatientWeight { get; set; }

        public DateTime StartTime { get; set; }
        public Financing Financing { get; set; }
        public PatientPosition PatientPosition { get; set; }
        public string Destination { get; set; }
        public TransportType TransportType { get; set; }

    }

}