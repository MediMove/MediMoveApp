using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Entities
{
    public class PersonalInformation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddres { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }

    }
}
