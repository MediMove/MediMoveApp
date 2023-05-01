using MediMove.Server.Models;
using Microsoft.EntityFrameworkCore;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Data
{
    public class MediMoveDbContext:DbContext
    {
        public DbSet<Salary> Salaries { get; set; }

        public MediMoveDbContext(DbContextOptions<MediMoveDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //////////////////////// PERSONAL INFO ///////////////////////////

            var personalInformation1 = new PersonalInformation
            {
                Id = 1,
                FirstName = "Pan",
                LastName = "Panowski",
                StreetAddress = "Kwiatowa",
                HouseNumber = "1",
                ApartmentNumber = 1,
                PostalCode = "41-100",
                StateProvince = "œl¹sk",
                Country = "Polska",
                PhoneNumber = "645839002",
                City = "Krakow"
            };

            var personalInformation2 = new PersonalInformation
            {
                Id = 2,
                FirstName = "Michal",
                LastName = "Jakistam",
                StreetAddress = "Sadowa",
                HouseNumber = "13",
                ApartmentNumber = 5,
                PostalCode = "42-800",
                StateProvince = "œl¹sk",
                Country = "Polska",
                PhoneNumber = "854888145",
                City = "Katowice"
            };

            var personalInformation3 = new PersonalInformation
            {
                Id = 3,
                FirstName = "Grzegorz",
                LastName = "Kowalski",
                StreetAddress = "Stara",
                HouseNumber = "3",
                ApartmentNumber = 4,
                PostalCode = "42-400",
                StateProvince = "wielkopolskie",
                Country = "Polska",
                PhoneNumber = "123123123",
                City = "Krakow"
            };

            var personalInformation4 = new PersonalInformation
            {
                Id = 4,
                FirstName = "Marek",
                LastName = "Zygmunt",
                StreetAddress = "Kwiatowa",
                HouseNumber = "44",

                PostalCode = "23-213",
                StateProvince = "wielkopolskie",
                Country = "Polska",
                PhoneNumber = "888888888",
                City = "Krakow"
            };

            var personalInformation5 = new PersonalInformation
            {
                Id = 5,
                FirstName = "Kamil",
                LastName = "Nowak",
                StreetAddress = "Rynek",
                HouseNumber = "10",

                PostalCode = "30-062",
                StateProvince = "ma³opolskie",
                Country = "Polska",
                PhoneNumber = "555777888",
                City = "Kraków"
            };

            var personalInformation6 = new PersonalInformation
            {
                Id = 6,
                FirstName = "Anna",
                LastName = "Kowalczyk",
                StreetAddress = "Polna",
                HouseNumber = "12",
                ApartmentNumber = 7,
                PostalCode = "80-850",
                StateProvince = "pomorskie",
                Country = "Polska",
                PhoneNumber = "789232737",
                City = "Gdañsk"
            };

            var personalInformation7 = new PersonalInformation
            {
                Id = 7,
                FirstName = "Marek",
                LastName = "Michalski",
                StreetAddress = "Jana Paw³a II",
                HouseNumber = "34",
                ApartmentNumber = 2,
                PostalCode = "01-001",
                StateProvince = "mazowieckie",
                Country = "Polska",
                PhoneNumber = "444555666",
                City = "Warszawa"
            };

            var personalInformation8 = new PersonalInformation
            {
                Id = 8,
                FirstName = "Magdalena",
                LastName = "Jankowska",
                StreetAddress = "Mickiewicza",
                HouseNumber = "17",
                ApartmentNumber = 5,
                PostalCode = "40-005",
                StateProvince = "œl¹skie",
                Country = "Polska",
                PhoneNumber = "777888999",
                City = "Katowice"
            };

            var personalInformation9 = new PersonalInformation
            {
                Id = 9,
                FirstName = "Pawe³",
                LastName = "Wójcik",
                StreetAddress = "S³owackiego",
                HouseNumber = "7",
                ApartmentNumber = 12,
                PostalCode = "50-049",
                StateProvince = "dolnoœl¹skie",
                Country = "Polska",
                PhoneNumber = "222333444",
                City = "Wroc³aw"
            };

            //-----------------------------------------------------
            modelBuilder.Entity<PersonalInformation>().HasData(personalInformation1);
            modelBuilder.Entity<PersonalInformation>().HasData(personalInformation2);
            modelBuilder.Entity<PersonalInformation>().HasData(personalInformation3);
            modelBuilder.Entity<PersonalInformation>().HasData(personalInformation4);
            modelBuilder.Entity<PersonalInformation>().HasData(personalInformation5);
            modelBuilder.Entity<PersonalInformation>().HasData(personalInformation6);
            modelBuilder.Entity<PersonalInformation>().HasData(personalInformation7);
            modelBuilder.Entity<PersonalInformation>().HasData(personalInformation8);
            modelBuilder.Entity<PersonalInformation>().HasData(personalInformation9);

            //////////////////////////////////////////////////////////////////

            /////////////////////////// PATIENTS //////////////////////////////

            var patient1 = new Patient
            {
                Id = 1,
                PersonalInformationId = 6,
                Weight = 60
            };

            var patient2 = new Patient
            {
                Id = 2,
                PersonalInformationId = 7,
                Weight = 78
            };

            var patient3 = new Patient
            {
                Id = 3,
                PersonalInformationId = 8,
                Weight = 55
            };

            var patient4 = new Patient
            {
                Id = 4,
                PersonalInformationId = 9,
                Weight = 93
            };

            //-----------------------------------------------------
            modelBuilder.Entity<Patient>().HasData(patient1);
            modelBuilder.Entity<Patient>().HasData(patient2);
            modelBuilder.Entity<Patient>().HasData(patient3);
            modelBuilder.Entity<Patient>().HasData(patient4);

            //////////////////////////////////////////////////////////////////


            ///////////////////////// PARAMEDICS /////////////////////////////

            var paramedic1 = new Paramedic
            {
                Id = 1,
                PersonalInformationId = 1,
                BankAccountNumber = "1203987908127474",
                IsDriver = true,
            };

            var paramedic2 = new Paramedic
            {
                Id = 2,
                PersonalInformationId = 2,
                BankAccountNumber = "124341763465609",
                IsDriver = true,
            };

            var paramedic3 = new Paramedic
            {
                Id = 3,
                PersonalInformationId = 3,
                BankAccountNumber = "121234124123109",
                IsDriver = false,
            };

            var paramedic4 = new Paramedic
            {
                Id = 4,
                PersonalInformationId = 4,
                BankAccountNumber = "123456780123109",
                IsDriver = false,
            };

            var paramedic5 = new Paramedic
            {
                Id = 5,
                PersonalInformationId = 3,
                BankAccountNumber = "982301231238812",
                IsDriver = true,
            };

            //-----------------------------------------------------
            modelBuilder.Entity<Paramedic>().HasData(paramedic1);
            modelBuilder.Entity<Paramedic>().HasData(paramedic2);
            modelBuilder.Entity<Paramedic>().HasData(paramedic3);
            modelBuilder.Entity<Paramedic>().HasData(paramedic4);
            modelBuilder.Entity<Paramedic>().HasData(paramedic5);

            //////////////////////////////////////////////////////////////////


            //////////////////////////// TEAMS ///////////////////////////////
            
            //Day:1

            var team1 = new Team
            {
                Id = 1,
                DriverId = 2,
                ParamedicId = 3,
                Day = DateTime.Today
            };

            //Day:2

            var team2 = new Team
            {
                Id = 2,
                DriverId = 5,
                ParamedicId = 4,
                Day = DateTime.Today.AddDays(1)
            };

            //Day:3

            var team3 = new Team
            {
                Id = 3,
                DriverId = 1,
                ParamedicId = 5,
                Day = DateTime.Today.AddDays(2)
            };

            //-----------------------------------------------------
            modelBuilder.Entity<Team>().HasData(team1);
            modelBuilder.Entity<Team>().HasData(team2);
            modelBuilder.Entity<Team>().HasData(team3);

            //////////////////////////////////////////////////////////////////


            ///////////////////////// TRANSPORTS /////////////////////////////

            //Team:1

            var transport1 = new Transport
            {
                Id = 1,
                TeamId = 1,
                PatientId = 1,
                PatientPosition = PatientPosition.Walking,
                TransportType = TransportType.Visit,
                Financing = Financing.FullyFunded,
                StartTime = DateTime.Today,
                Destination = "Rybnik"
            };
            
            var transport2 = new Transport
            {
                Id = 2,
                TeamId = 1,
                PatientId = 2,
                PatientPosition = PatientPosition.Sitting,
                TransportType = TransportType.Visit,
                Financing = Financing.FullyPaid,
                StartTime = DateTime.Today,
                Destination = "Mys³owice"
            };

            var transport3 = new Transport
            {
                Id = 3,
                TeamId = 1,
                PatientId = 3,
                PatientPosition = PatientPosition.Sitting,
                TransportType = TransportType.Handover,
                Financing = Financing.PartiallyFunded,
                StartTime = DateTime.Today,
                Destination = "Bytom"
            };
            
            //Team:2

            var transport4 = new Transport
            {
                Id = 4,
                TeamId = 2,
                PatientId = 1,
                PatientPosition = PatientPosition.Walking,
                TransportType = TransportType.Handover,
                Financing = Financing.PartiallyFunded,
                StartTime = DateTime.Today.AddDays(1),
                Destination = "Zabrze"
            };
            
            var transport5 = new Transport
            {
                Id = 5,
                TeamId = 2,
                PatientId = 4,
                PatientPosition = PatientPosition.Walking,
                TransportType = TransportType.Visit,
                Financing = Financing.FullyFunded,
                StartTime = DateTime.Today.AddDays(1),
                Destination = "Chorzów"
            };
            
            //Team:3

            var transport6 = new Transport
            {
                Id = 6,
                TeamId = 3,
                PatientId = 4,
                PatientPosition = PatientPosition.Walking,
                TransportType = TransportType.Visit,
                Financing = Financing.FullyFunded,
                StartTime = DateTime.Today.AddDays(2),
                Destination = "Rybnik"
            };

            //-----------------------------------------------------
            modelBuilder.Entity<Transport>().HasData(transport1);
            modelBuilder.Entity<Transport>().HasData(transport2);
            modelBuilder.Entity<Transport>().HasData(transport3);
            modelBuilder.Entity<Transport>().HasData(transport4);
            modelBuilder.Entity<Transport>().HasData(transport5);
            modelBuilder.Entity<Transport>().HasData(transport6);

            //////////////////////////////////////////////////////////////////



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
