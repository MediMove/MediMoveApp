using ErrorOr;
using MediatR;

namespace MediMove.Shared.Models.DTOs
{
    //public record CreateTeamDTO(int DriverId, int ParamedicId, DateOnly Day);

    public class CreateTeamDTO
    {
        public int DriverId { get; set; }
        public int ParamedicId { get; set; }
        public DateTime Day { get; set; }
    }
}
