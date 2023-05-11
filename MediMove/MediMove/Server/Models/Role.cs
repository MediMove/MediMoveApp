namespace MediMove.Server.Models
{
    public class Role
    {
        public int Id { get; set; }
        public int? UserAccountMapId { get; set; }
        public string Name { get; set; }
    }
}
