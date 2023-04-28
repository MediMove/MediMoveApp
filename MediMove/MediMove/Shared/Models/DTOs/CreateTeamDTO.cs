namespace MediMove.Shared.Models.DTOs
{
    public class CreateTeamDTO
    {
        public int DriverId { get; set; }
        public int ParamedicId { get; set; }
        public DateOnly Day { get; set; }
    }
}
