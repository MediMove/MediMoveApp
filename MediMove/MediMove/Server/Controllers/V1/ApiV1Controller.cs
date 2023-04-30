using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ApiV1Controller : ControllerBase
    {

    }
}
