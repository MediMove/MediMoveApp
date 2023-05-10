namespace MediMove.Server.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException(string message) : base($"Invalid date provided. " + message + ".") { }
    }
}
