using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class ParamedicDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddres { get; set; }
        public string HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string BankAccountNumber { get; set; }
        public bool IsDriver { get; set; }
        public string PhoneNumber { get; set; }
    }
}
