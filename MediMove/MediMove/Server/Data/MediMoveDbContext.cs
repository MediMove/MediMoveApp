using MediMove.Server.Entities;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Data
{
    public class MediMoveDbContext:DbContext
    {
        public MediMoveDbContext(DbContextOptions<MediMoveDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().HasData(new Patient
            {
                Id = 1,
                PersonalInformationId = 77,
                Weight = 40
            }
            );

            modelBuilder.Entity<PersonalInformation>().HasData(new PersonalInformation
            {
                Id = 77,
                FirstName = "Pan",
                LastName = "Panowski",
                StreetAddres = "Kwiatowa",
                HouseNumber = "1",
                ApartmentNumber = 1,
                PostalCode = "41-100",
                StateProvince = "slask",
                Country = "Polska"
            });

            modelBuilder.Entity<Paramedic>().HasData(new Paramedic
            {
                Id = 1,
                PersonalInformationId = 88,
                BankAccountNumber = "123123",
                IsDriver = true,
                PhoneNumber = "123123123"
            });

            modelBuilder.Entity<PersonalInformation>().HasData(new PersonalInformation
            {
                Id = 88,
                FirstName = "Grzegorz",
                LastName = "Kowalski",
                StreetAddres = "Stara",
                HouseNumber = "3",
                ApartmentNumber = 4,
                PostalCode = "42-400",
                StateProvince = "slask",
                Country = "Polska"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                Id=1,
                DriverId = 1,
                ParamedicId = 1,
                Day = DateTime.Today
            });

            modelBuilder.Entity<Transport>().HasData(new Transport
            {
                Id = 1,
                TeamId = 1,
                PatientId = 1,
                PatientPosition = PatientPosition.Walking,
                Financing = Financing.FullyFunded,
                StartTime = DateTime.Today,
                Destination = "Morawy"
            });
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Availability> Availabilities { get; set; }

        public DbSet<Dispatcher> Dispatchers { get; set; }

        public DbSet<Paramedic> Paramedics { get; set; }

        public DbSet<PersonalInformation> PersonalInformations { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<Billing> Billings { get; set; }
    }
}
