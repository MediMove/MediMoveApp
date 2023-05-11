using MediMove.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class RatesSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            // Warining: No store type was specified for the decimal property 'PayPerHour' on entity type 'Rate'
            modelBuilder.Entity<Rate>()
                .Property(e => e.PayPerHour)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Rate>().HasData(new List<Rate>
            {
                // Starting rates
                new Rate
                {
                    Id = 1,
                    Date = DateTime.Today.AddDays(-2),
                    PayPerHour = 12,
                    ParamedicId = 1,
                },

                new Rate
                {
                    Id = 2,
                    Date = DateTime.Today.AddDays(-2),
                    PayPerHour = 12,
                    ParamedicId = 2,
                },

                new Rate
                {
                    Id = 3,
                    Date = DateTime.Today.AddDays(-2),
                    PayPerHour = 12,
                    ParamedicId = 3,
                },

                new Rate
                {
                    Id = 4,
                    Date = DateTime.Today.AddDays(-2),
                    PayPerHour = 12,
                    ParamedicId = 4,
                },

                new Rate
                {
                    Id = 5,
                    Date = DateTime.Today.AddDays(-2),
                    PayPerHour = 12,
                    ParamedicId = 5,
                },

                // Raises yesterday
                new Rate
                {
                    Id = 6,
                    Date = DateTime.Today.AddDays(-1),
                    PayPerHour = 13,
                    ParamedicId = 1,
                },

                new Rate
                {
                    Id = 7,
                    Date = DateTime.Today.AddDays(-1),
                    PayPerHour = 13,
                    ParamedicId = 5,
                },

                // Raise effective from tomorrow
                new Rate
                {
                    Id = 8,
                    Date = DateTime.Today.AddDays(1),
                    PayPerHour = 14,
                    ParamedicId = 1,
                },
            });
        }
    }
}
