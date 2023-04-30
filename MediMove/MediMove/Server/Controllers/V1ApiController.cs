using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class V1ApiController : ControllerBase
    {

    }
}
