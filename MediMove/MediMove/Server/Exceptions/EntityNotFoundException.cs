
namespace MediMove.Server.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(Type entityType, int entityId) :
            base($"The {entityType.Name} with ID {entityId} could not be found.") { }
    }
}
