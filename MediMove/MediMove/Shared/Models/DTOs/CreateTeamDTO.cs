using ErrorOr;
using MediatR;

namespace MediMove.Shared.Models.DTOs
{
    public record CreateTeamDTO(int DriverId, int ParamedicId, DateOnly Day);
    //{
    //    public int DriverId { get; set; }
    //    public int ParamedicId { get; set; }
    //    public DateOnly Day { get; set; }
    //}
}
