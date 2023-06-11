using MediMove.Server.Application.Models;
using MediMove.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class TransportsSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transport>().HasData(new List<Transport>
            {
                // Ereyesterday
                // Team 1
                new Transport
                {
                    Id = 1,
                    TeamId = 1,
                    PatientId = 1,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(-2).Year, DateTime.Today.AddDays(-2).Month, DateTime.Today.AddDays(-2).Day, 7, 0, 0),
                    Destination =  "Saska 43 Rybnik"
                },
                new Transport
                {
                    Id = 2,
                    TeamId = 1,
                    PatientId = 2,
                    PatientPosition = PatientPosition.Sitting,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyPaid,
                    BillingId = 1,
                    StartTime = new DateTime(DateTime.Today.AddDays(-2).Year, DateTime.Today.AddDays(-2).Month, DateTime.Today.AddDays(-2).Day, 9, 10, 0),
                    Destination = "Nadrzeczna 55 Mysłowice"

                },
                new Transport
                {
                    Id = 3,
                    TeamId = 1,
                    PatientId = 3,
                    PatientPosition = PatientPosition.Sitting,
                    TransportType = TransportType.Handover,
                    Financing = Financing.PartiallyFunded,
                    BillingId = 2,
                    StartTime = new DateTime(DateTime.Today.AddDays(-2).Year, DateTime.Today.AddDays(-2).Month, DateTime.Today.AddDays(-2).Day, 14, 0, 0),
                    Destination = "Wyszogrodzka 44 Bytom"
                },

                // Team 2
                new Transport
                {
                    Id = 4,
                    TeamId = 2,
                    PatientId = 4,
                    PatientPosition = PatientPosition.Sitting,
                    TransportType = TransportType.Handover,
                    Financing = Financing.PartiallyFunded,
                    BillingId = 3,
                    StartTime = new DateTime(DateTime.Today.AddDays(-2).Year, DateTime.Today.AddDays(-2).Month, DateTime.Today.AddDays(-2).Day, 7, 30, 0),
                    Destination = "Dobosza 101 Zabrze"
                },
                new Transport
                {
                    Id = 5,
                    TeamId = 2,
                    PatientId = 5,
                    PatientPosition = PatientPosition.Lying,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(-2).Year, DateTime.Today.AddDays(-2).Month, DateTime.Today.AddDays(-2).Day, 12, 0, 0),
                    Destination = "Dyngus 30 Chorzów"
                },

                // Yesterday
                // Team 3
                new Transport
                {
                    Id = 6,
                    TeamId = 3,
                    PatientId = 4,
                    PatientPosition = PatientPosition.Sitting,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(-1).Year, DateTime.Today.AddDays(-1).Month, DateTime.Today.AddDays(-1).Day, 7, 30, 0),
                    Destination = "Obornicka 89 Rybnik"
                },
                new Transport
                {
                    Id = 7,
                    TeamId = 3,
                    PatientId = 2,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(-1).Year, DateTime.Today.AddDays(-1).Month, DateTime.Today.AddDays(-1).Day, 9, 45, 0),
                    Destination = "Zakładowa 19 Częstochowa"
                },
                new Transport
                {
                    Id = 8,
                    TeamId = 3,
                    PatientId = 1,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(-1).Year, DateTime.Today.AddDays(-1).Month, DateTime.Today.AddDays(-1).Day, 13, 0, 0),
                    Destination = "Ustronna 70 Gliwice"
                },

                // Team 4
                new Transport
                {
                    Id = 9,
                    TeamId = 4,
                    PatientId = 4,
                    PatientPosition = PatientPosition.Sitting,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(-1).Year, DateTime.Today.AddDays(-1).Month, DateTime.Today.AddDays(-1).Day, 15, 30, 0),
                    Destination = "Oboźna 43 Tychy"
                },
                new Transport
                {
                    Id = 10,
                    TeamId = 4,
                    PatientId = 2,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(-1).Year, DateTime.Today.AddDays(-1).Month, DateTime.Today.AddDays(-1).Day, 18, 0, 0),
                    Destination = "Mickiewicza 119 Żory"
                },
                new Transport
                {
                    Id = 11,
                    TeamId = 4,
                    PatientId = 5,
                    PatientPosition = PatientPosition.Lying,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(-1).Year, DateTime.Today.AddDays(-1).Month, DateTime.Today.AddDays(-1).Day, 20, 0, 0),
                    Destination = "Rycerska 13 Sosnowiec"
                },

                // Today
                // Team 5
                new Transport
                {
                    Id = 12,
                    TeamId = 5,
                    PatientId = 1,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 7, 0, 0),
                    Destination = "Skromna 116 Bieruń"
                },
                new Transport
                {
                    Id = 13,
                    TeamId = 5,
                    PatientId = 3,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0),
                    Destination = "Lidzka 53 Świętochłowice"
                },
                new Transport
                {
                    Id = 14,
                    TeamId = 5,
                    PatientId = 2,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 13, 30, 0),
                    Destination = "Pomarańczowa 95 Bielsko-Biała"
                },

                // Team 6
                new Transport
                {
                    Id = 15,
                    TeamId = 6,
                    PatientId = 4,
                    PatientPosition = PatientPosition.Sitting,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 16, 0, 0),
                    Destination = "Bydgoska 123 Ruda Śląska"
                },
                new Transport
                {
                    Id = 16,
                    TeamId = 6,
                    PatientId = 2,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 17, 45, 0),
                    Destination = "Różana 138 Żory"
                },
                new Transport
                {
                    Id = 17,
                    TeamId = 6,
                    PatientId = 4,
                    PatientPosition = PatientPosition.Sitting,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 19, 0, 0),
                    Destination = "Okrzei Stefana 132 Chorzów"
                },

                // Tomorrow
                // Team 7
                new Transport
                {
                    Id = 18,
                    TeamId = 7,
                    PatientId = 1,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(1).Year, DateTime.Today.AddDays(1).Month, DateTime.Today.AddDays(1).Day, 7, 15, 0),
                    Destination = "Diamentowa 5 Częstochowa"
                },
                new Transport
                {
                    Id = 19,
                    TeamId = 7,
                    PatientId = 2,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(1).Year, DateTime.Today.AddDays(1).Month, DateTime.Today.AddDays(1).Day, 11, 0, 0),
                    Destination = "Pawlikowskiego Tadeusza 96 Cieszyn"
                },
                new Transport
                {
                    Id = 20,
                    TeamId = 7,
                    PatientId = 3,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(1).Year, DateTime.Today.AddDays(1).Month, DateTime.Today.AddDays(1).Day, 13, 15, 0),
                    Destination = "Generała Szyllinga Antoniego 111 Imielin"
                },

                // Team 8
                new Transport
                {
                    Id = 21,
                    TeamId = 8,
                    PatientId = 5,
                    PatientPosition = PatientPosition.Lying,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(1).Year, DateTime.Today.AddDays(1).Month, DateTime.Today.AddDays(1).Day, 16, 0, 0),
                    Destination = "Urbańskiego Tadeusza 45 Pszów"
                },
                new Transport
                {
                    Id = 22,
                    TeamId = 8,
                    PatientId = 4,
                    PatientPosition = PatientPosition.Sitting,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(1).Year, DateTime.Today.AddDays(1).Month, DateTime.Today.AddDays(1).Day, 17, 15, 0),
                    Destination = "Czerwona 46 Katowice"
                },
                new Transport
                {
                    Id = 23,
                    TeamId = 8,
                    PatientId = 3,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(1).Year, DateTime.Today.AddDays(1).Month, DateTime.Today.AddDays(1).Day, 20, 0, 0),
                    Destination = "Reutta 54 Bytom"
                },

                // Day after tomorrow
                // Team 9
                new Transport
                {
                    Id = 24,
                    TeamId = 9,
                    PatientId = 1,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(2).Year, DateTime.Today.AddDays(2).Month, DateTime.Today.AddDays(2).Day, 8, 30, 0),
                    Destination = "Cybernetyków 120 Tychy"
                },
                new Transport
                {
                    Id = 25,
                    TeamId = 9,
                    PatientId = 3,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(2).Year, DateTime.Today.AddDays(2).Month, DateTime.Today.AddDays(2).Day, 12, 0, 0),
                    Destination = "Kaszubska 18 Świętochłowice"
                },

                // Team 10
                new Transport
                {
                    Id = 26,
                    TeamId = 10,
                    PatientId = 2,
                    PatientPosition = PatientPosition.Walking,
                    TransportType = TransportType.Handover,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(2).Year, DateTime.Today.AddDays(2).Month, DateTime.Today.AddDays(2).Day, 17, 0, 0),
                    Destination = "Cybernetyków 120 Tychy"
                },
                new Transport
                {
                    Id = 27,
                    TeamId = 10,
                    PatientId = 5,
                    PatientPosition = PatientPosition.Lying,
                    TransportType = TransportType.Visit,
                    Financing = Financing.FullyFunded,
                    StartTime = new DateTime(DateTime.Today.AddDays(2).Year, DateTime.Today.AddDays(2).Month, DateTime.Today.AddDays(2).Day, 19, 0, 0),
                    Destination = "Kaszubska 18 Świętochłowice"
                },
            });
        }
    }
}
