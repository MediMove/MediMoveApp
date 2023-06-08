
namespace MediMove.Server.Application.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal PayPerHour { get; set; }
        public int ParamedicId { get; set; }
        public virtual Paramedic Paramedic { get; set; }
    }
}
