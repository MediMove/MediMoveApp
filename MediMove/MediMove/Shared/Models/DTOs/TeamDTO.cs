namespace MediMove.Shared.Models.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public DateOnly Day { get; set; }
        public int DriverId { get; set; }
        public int ParamedicId { get; set; }
    }
}
