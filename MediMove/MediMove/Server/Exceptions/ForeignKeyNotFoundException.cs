namespace MediMove.Server.Exceptions
{
    public class ForeignKeyNotFoundException : Exception
    {
        public ForeignKeyNotFoundException(string message) : base(message) { }
        public ForeignKeyNotFoundException(Type parentType, int parentId, Type childType) :
            base($"The {childType.Name} with a foreign key to the {parentType.Name} with ID {parentId} could not be found.")
        { }
    }
}
