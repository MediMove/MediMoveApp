using System.ComponentModel.DataAnnotations.Schema;

namespace MediMove.Server.Models
{
    
    public class Dispatcher
    {
        public int Id { get; set; }
        public string BankAccountNumber { get; set; }
        virtual public ICollection<Salary> Salaries { get; set; }
        public int PersonalInformationId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
    }
}
