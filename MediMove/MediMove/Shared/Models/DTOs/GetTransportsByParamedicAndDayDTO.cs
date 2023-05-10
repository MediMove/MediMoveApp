
using ErrorOr;
using MediatR;

namespace MediMove.Shared.Models.DTOs
{
    public class GetTransportsByParamedicAndDayDTO : IRequest<ErrorOr<IEnumerable<TransportDTO>>>
    {
        public int ParamedicId { get; set; }
        public DateTime Day { get; set; }
    }
}
