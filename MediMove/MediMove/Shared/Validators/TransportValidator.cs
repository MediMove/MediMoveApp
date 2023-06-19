
namespace MediMove.Shared.Validators
{
    public static class TransportValidator
    {
        public static bool CanExecuteCommands(DateTime transportStartTime) =>
            transportStartTime >= DateTime.Today && transportStartTime <= DateTime.Today.AddYears(1);

        public static bool CanAssignTeam(DateTime transportStartTime) =>
            CanExecuteCommands(transportStartTime);

        public static bool CanCancelTransport(DateTime transportStartTime) =>
            CanExecuteCommands(transportStartTime);
    }
}
