using MediMove.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class PatientsSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(new List<Patient>
            {
                new Patient
                {
                    Id = 1,
                    PersonalInformationId = 6,
                    Weight = 60,
                },

                new Patient
                {
                    Id = 2,
                    PersonalInformationId = 7,
                    Weight = 78,
                },

                new Patient
                {
                    Id = 3,
                    PersonalInformationId = 8,
                    Weight = 55,
                },

                new Patient
                {
                    Id = 4,
                    PersonalInformationId = 9,
                    Weight = 93,
                },
            });
        }
    }
}
