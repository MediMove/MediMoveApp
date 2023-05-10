using ErrorOr;
using MediatR;

namespace MediMove.Shared.Models.DTOs
{
    public class CreateTeamDTO : IRequest<ErrorOr<int>>
    {
        public int DriverId { get; set; }
        public int ParamedicId { get; set; }
        public DateOnly Day { get; set; }
    }
}
