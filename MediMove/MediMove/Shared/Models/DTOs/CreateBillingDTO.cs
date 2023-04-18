using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs
{
    public class CreateBilling
    {
        public int PersonalInformationId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal Cost { get; set; }
    }
}
