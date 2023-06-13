using MediMove.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.DTOs;

public class GetTransportsResponse
{
    public Dictionary<int, TransportDTO> Transports { get; set; }

}