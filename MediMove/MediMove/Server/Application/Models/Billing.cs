using System.ComponentModel.DataAnnotations.Schema;

namespace MediMove.Server.Application.Models
{
    
    public class Billing
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal Cost { get; set; }
        public int PersonalInformationId { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }
    }
}
