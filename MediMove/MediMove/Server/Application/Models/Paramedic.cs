
namespace MediMove.Server.Application.Models
{
    public class Paramedic
    {
        public int Id { get; set; }
        public string BankAccountNumber { get; set; }
        public bool IsDriver { get; set; }
        public bool IsWorking { get; set; } = true;
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Availability> Availabilities { get; set; }
        public int PersonalInformationId { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }
    }
}
