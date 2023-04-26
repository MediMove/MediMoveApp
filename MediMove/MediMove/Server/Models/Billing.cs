using System.ComponentModel.DataAnnotations.Schema;

namespace MediMove.Server.Models
{
    
    public class Billing
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal Cost { get; set; }
        public int PersonalInformationId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
    }
}
