
namespace MediMove.Server.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(Type entityType) :
            base($"The {entityType.Name} could not be found.") { }
        public EntityNotFoundException(Type entityType, int entityId) :
            base($"The {entityType.Name} with ID {entityId} could not be found.") { }
    }
}
