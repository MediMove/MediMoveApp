namespace MediMove.Server.Application.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? AccountId { get; set; }
        public int RoleId { get; set; } = 1;
        public virtual Role Role { get; set; }
        

    }
}
