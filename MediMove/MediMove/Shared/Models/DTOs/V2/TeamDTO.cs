namespace MediMove.Shared.Models.DTOs.V2
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int DriverId { get; set; }
        public int ParamedicId { get; set; }
    }
}
