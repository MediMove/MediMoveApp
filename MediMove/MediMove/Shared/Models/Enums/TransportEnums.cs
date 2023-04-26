using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMove.Shared.Models.Enums
{
    public enum PatientPosition
    {
        Walking,
        Sitting,
        Lying
    }

    public enum Financing
    {
        FullyFunded,
        PartiallyFunded,
        FullyPaid
    }

    public enum TransportType
    {
        Visit,
        Handover
    }
}
