namespace MediMove.Server.Models
{
    public class UserLoginData
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
