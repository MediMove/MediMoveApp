namespace MediMove.Client.temp
{
    public record MediMoveResponse
    {
        public HttpResponseMessage? HttpResponse { get; } 
        public ErrorResponse? ErrorResponse { get; }
        public MediMoveResponse()
        {
            HttpResponse = null;
            ErrorResponse = null;
        }
        public MediMoveResponse(HttpResponseMessage response)
        {
            HttpResponse = response;
            ErrorResponse = null;
        }

        public MediMoveResponse(ErrorResponse response)
        {
            ErrorResponse = response;
            HttpResponse = null;
        }
    }
}
