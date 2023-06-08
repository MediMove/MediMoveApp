using MediMove.Server.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class RoleSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new List<Role>
            {
                new Role
                {
                    RoleId = 1,
                    Name = "Undefined"
                },

                new Role
                {
                    RoleId = 2,
                    Name = "Paramedic"
                },
                new Role
                {
                    RoleId = 3,
                    Name ="Dispatcher"

                },
                new Role
                {
                    RoleId = 4,
                    Name = "Admin"
                }
            });
        }
    }
}
