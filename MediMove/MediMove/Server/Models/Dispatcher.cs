using System.ComponentModel.DataAnnotations.Schema;

namespace MediMove.Server.Models
{
    
    public class Dispatcher
    {
        public int Id { get; set; }
        public string BankAccountNumber { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public int PersonalInformationId { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }
    }
}
