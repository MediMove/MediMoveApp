using System.Net;

namespace MediMove.Client.temp
{
    public class StandardResponse
    {
        public readonly HttpStatusCode statusCode;
        public StandardResponse(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }
    }
}
