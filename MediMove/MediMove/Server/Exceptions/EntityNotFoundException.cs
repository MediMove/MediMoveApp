
namespace MediMove.Server.Exceptions
{
    /// <summary>
    /// Exception thrown when requesting a non-existent record.
    /// </summary>
    public class EntityNotFoundException : NotFoundException
    {
        /// <summary>
        /// A constructor that generates a message about the context of the exception.
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="entityId">Entity ID</param>
        public EntityNotFoundException(Type entityType, int entityId) :
            base($"{entityType.Name} entity with ID {entityId} was not found.") { }
    }
}
