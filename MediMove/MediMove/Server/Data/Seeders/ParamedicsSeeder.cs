using MediMove.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class ParamedicsSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paramedic>().HasData(new List<Paramedic>
            {
                new Paramedic
                {
                    Id = 1,
                    PersonalInformationId = 1,
                    BankAccountNumber = "1203987908127474",
                    IsDriver = true,
                },

                new Paramedic
                {
                    Id = 2,
                    PersonalInformationId = 2,
                    BankAccountNumber = "124341763465609",
                    IsDriver = true,
                },

                new Paramedic
                {
                    Id = 3,
                    PersonalInformationId = 3,
                    BankAccountNumber = "121234124123109",
                    IsDriver = false,
                },

                new Paramedic
                {
                    Id = 4,
                    PersonalInformationId = 4,
                    BankAccountNumber = "123456780123109",
                    IsDriver = false,
                },

                new Paramedic
                {
                    Id = 5,
                    PersonalInformationId = 5,
                    BankAccountNumber = "982301231238812",
                    IsDriver = true,
                },
            });
        }
    }
}
