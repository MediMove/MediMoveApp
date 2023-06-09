﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    
    public class GetPatientsByDateAndPaymentsSumDTO
    {
        public class GetPatientsByDateAndPaymentsSumRow
        {
            public int Id { get; set; }
            public int Weight { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            //public string StreetAddress { get; set; }
            //public string HouseNumber { get; set; }
            //public int? ApartmentNumber { get; set; }
            //public string City { get; set; }
            //public string PostalCode { get; set; }
            //public string StateProvince { get; set; }
            public string Country { get; set; }
            public string PhoneNumber { get; set; }
            public decimal PaymentsSum { get; set; }    
        }
        public List<GetPatientsByDateAndPaymentsSumRow> Rows { get; set; }

    }
     
}
