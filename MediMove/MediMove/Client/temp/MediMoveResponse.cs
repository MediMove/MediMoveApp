namespace MediMove.Client.temp
{
    public record MediMoveResponse<T> where T : class
    {
        public ErrorResponse? ErrorResponse { get; }
        public T? CorrectResponse { get; }

        public MediMoveResponse()
        {
            ErrorResponse = null;
            CorrectResponse = null; //default(T!)
        }

        public MediMoveResponse(ErrorResponse response)
        {
            ErrorResponse = response;
            CorrectResponse = null;
        }

        public MediMoveResponse(T response)
        {
            ErrorResponse = null;
            CorrectResponse = response;
        }
        /* Base response - zrobic klase abstrakcyjna która bedzie musiala byc zaimplementowana poprzez klasy response, Ta implementacja zamienić na MedioveResponse */
    }
}
