using MediMove.Server.Application.Models;
using MediMove.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class TeamsSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(new List<Team>
            {
                // Ereyesterday
                new Team
                {
                    Id = 1,
                    DriverId = 1,
                    ParamedicId = 3,
                    Day = DateTime.Today.AddDays(-2),
                    ShiftType = ShiftType.Morning,
                },
                new Team
                {
                    Id = 2,
                    DriverId = 5,
                    ParamedicId = 4,
                    Day = DateTime.Today.AddDays(-2),
                    ShiftType = ShiftType.Evening,
                },

                // Yesterday
                new Team
                {
                    Id = 3,
                    DriverId = 1,
                    ParamedicId = 2,
                    Day = DateTime.Today.AddDays(-1),
                    ShiftType = ShiftType.Morning,
                },
                new Team
                {
                    Id = 4,
                    DriverId = 5,
                    ParamedicId = 3,
                    Day = DateTime.Today.AddDays(-1),
                    ShiftType = ShiftType.Evening,
                },

                // Today
                new Team
                {
                    Id = 5,
                    DriverId = 1,
                    ParamedicId = 2,
                    Day = DateTime.Today,
                    ShiftType = ShiftType.Morning,
                },
                new Team
                {
                    Id = 6,
                    DriverId = 5,
                    ParamedicId = 4,
                    Day = DateTime.Today,
                    ShiftType = ShiftType.Evening,
                },

                // Tomorrow
                new Team
                {
                    Id = 7,
                    DriverId = 1,
                    ParamedicId = 3,
                    Day = DateTime.Today.AddDays(1),
                    ShiftType = ShiftType.Morning,
                },
                new Team
                {
                    Id = 8,
                    DriverId = 5,
                    ParamedicId = 4,
                    Day = DateTime.Today.AddDays(1),
                    ShiftType = ShiftType.Evening,
                },

                // Day after tomorrow
                new Team
                {
                    Id = 9,
                    DriverId = 1,
                    ParamedicId = 4,
                    Day = DateTime.Today.AddDays(2),
                    ShiftType = ShiftType.Morning,
                },
                new Team
                {
                    Id = 10,
                    DriverId = 2,
                    ParamedicId = 3,
                    Day = DateTime.Today.AddDays(2),
                    ShiftType = ShiftType.Evening,
                },
            });
        }
    }
}