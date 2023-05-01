using MediMove.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class SalariesSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            // Warining: No store type was specified for the decimal property 'Income' on entity type 'Salary'
            modelBuilder.Entity<Salary>()
                .Property(e => e.Income)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Salary>().HasData(new List<Salary>
            {
                new Salary
                {
                    Id = 1,
                    Date = DateTime.Today.AddDays(-2),
                    Income = 1200,
                    DispatcherId = 1,
                },

                new Salary
                {
                    Id = 2,
                    Date = DateTime.Today.AddDays(-2),
                    Income = 1200,
                    DispatcherId = 2,
                },

                new Salary
                {
                    Id = 3,
                    Date = DateTime.Today.AddDays(-1),
                    Income = 1300,
                    DispatcherId = 1,
                },

                // Raise effective from tomorrow
                new Salary
                {
                    Id = 4,
                    Date = DateTime.Today.AddDays(1),
                    Income = 1500,
                    DispatcherId = 1,
                },
            });
        }
    }
}
