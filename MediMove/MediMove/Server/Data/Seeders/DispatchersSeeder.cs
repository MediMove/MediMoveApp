using MediMove.Server.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class DispatchersSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dispatcher>().HasData(new List<Dispatcher>
            {
                new Dispatcher
                {
                    Id = 1,
                    BankAccountNumber = "4203987928122474",
                    PersonalInformationId = 13,
                },

                new Dispatcher
                {
                    Id = 2,
                    BankAccountNumber = "4203787958122274",
                    PersonalInformationId = 14,
                },
            });
        }
    }
}
