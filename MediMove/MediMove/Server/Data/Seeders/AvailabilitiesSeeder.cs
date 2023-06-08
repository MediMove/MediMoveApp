using MediMove.Server.Application.Models;
using MediMove.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class AvailabilitiesSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Availability>().HasData(new List<Availability>
            {
                // Ereyesterday
                new Availability
                {
                    Id = 1,
                    Day = DateTime.Today.AddDays(-2),
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 1,
                },
                new Availability
                {
                    Id = 2,
                    Day = DateTime.Today.AddDays(-2),
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 3,
                },
                new Availability
                {
                    Id = 3,
                    Day = DateTime.Today.AddDays(-2),
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 4,
                },
                new Availability
                {
                    Id = 4,
                    Day = DateTime.Today.AddDays(-2),
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 5,
                },

                // Yesterday
                new Availability
                {
                    Id = 5,
                    Day = DateTime.Today.AddDays(-1),
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 1,
                },
                new Availability
                {
                    Id = 6,
                    Day = DateTime.Today.AddDays(-1),
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 2,
                },
                new Availability
                {
                    Id = 7,
                    Day = DateTime.Today.AddDays(-1),
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 3,
                },
                new Availability
                {
                    Id = 8,
                    Day = DateTime.Today.AddDays(-1),
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 4,
                },
                new Availability
                {
                    Id = 9,
                    Day = DateTime.Today.AddDays(-1),
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 5,
                },

                // Today
                new Availability
                {
                    Id = 10,
                    Day = DateTime.Today,
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 1,
                },
                new Availability
                {
                    Id = 11,
                    Day = DateTime.Today,
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 2,
                },
                new Availability
                {
                    Id = 12,
                    Day = DateTime.Today,
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 3,
                },
                new Availability
                {
                    Id = 13,
                    Day = DateTime.Today,
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 4,
                },
                new Availability
                {
                    Id = 14,
                    Day = DateTime.Today,
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 5,
                },

                // Tomorrow
                new Availability
                {
                    Id = 15,
                    Day = DateTime.Today.AddDays(1),
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 1,
                },
                new Availability
                {
                    Id = 16,
                    Day = DateTime.Today.AddDays(1),
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 3,
                },
                new Availability
                {
                    Id = 17,
                    Day = DateTime.Today.AddDays(1),
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 4,
                },
                new Availability
                {
                    Id = 18,
                    Day = DateTime.Today.AddDays(1),
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 5,
                },

                // Day after tomorrow
                new Availability
                {
                    Id = 19,
                    Day = DateTime.Today.AddDays(2),
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 1,
                },
                new Availability
                {
                    Id = 20,
                    Day = DateTime.Today.AddDays(2),
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 2,
                },
                new Availability
                {
                    Id = 21,
                    Day = DateTime.Today.AddDays(2),
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 3,
                },
                new Availability
                {
                    Id = 22,
                    Day = DateTime.Today.AddDays(2),
                    ShiftType = ShiftType.Morning,
                    ParamedicId = 4,
                },
                new Availability
                {
                    Id = 23,
                    Day = DateTime.Today.AddDays(2),
                    ShiftType = ShiftType.Evening,
                    ParamedicId = 5,
                },
            });
        }
    }
}
