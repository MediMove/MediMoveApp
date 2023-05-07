using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ApiController : ControllerBase
    {
    }
}
