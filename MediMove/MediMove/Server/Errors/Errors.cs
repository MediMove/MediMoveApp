using ErrorOr;

namespace MediMove.Server.Errors
{
    public static class Errors
    {
        public static Error EntityNotFoundError => Error.NotFound("NotFound.Entity", "Entity not found.");
        public static Error MappingError => Error.Failure();
    }
}
