using ErrorOr;

namespace MediMove.Server.Application.Errors
{
    public static class Errors
    {
        public static Error EntityNotFoundError => Error.NotFound( "ENTITY_NOT_FOUND", "Entity not found");
        public static Error MappingError => Error.Failure();

        public static Error LoginError => Error.Custom(401, "LOGIN_ERROR", "Invalid username or password");
    }
}
