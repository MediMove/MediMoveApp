using MediMove.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Data.Seeders
{
    public class PersonalInformationsSeeder : IDbSeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalInformation>().HasData(new List<PersonalInformation>
            {
                // patient
                new PersonalInformation
                {
                    Id = 6,
                    FirstName = "Anna",
                    LastName = "Kowalczyk",
                    StreetAddress = "Polna",
                    HouseNumber = "12",
                    ApartmentNumber = 7,
                    PostalCode = "80-255",
                    StateProvince = "pomorskie",
                    Country = "Polska",
                    PhoneNumber = "789232737",
                    City = "Gdańsk",
                },

                // patient
                new PersonalInformation
                {
                    Id = 7,
                    FirstName = "Marek",
                    LastName = "Michalski",
                    StreetAddress = "Jana Pawła II",
                    HouseNumber = "34",
                    ApartmentNumber = 2,
                    PostalCode = "00-207",
                    StateProvince = "mazowieckie",
                    Country = "Polska",
                    PhoneNumber = "444555666",
                    City = "Warszawa",
                },

                // patient
                new PersonalInformation
                {
                    Id = 8,
                    FirstName = "Magdalena",
                    LastName = "Jankowska",
                    StreetAddress = "Mickiewicza",
                    HouseNumber = "17",
                    ApartmentNumber = 5,
                    PostalCode = "40-009",
                    StateProvince = "śląskie",
                    Country = "Polska",
                    PhoneNumber = "777888999",
                    City = "Katowice",
                },

                // patient
                new PersonalInformation
                {
                    Id = 9,
                    FirstName = "Paweł",
                    LastName = "Wójcik",
                    StreetAddress = "Słowackiego",
                    HouseNumber = "7",
                    ApartmentNumber = 12,
                    PostalCode = "50-049",
                    StateProvince = "dolnośląskie",
                    Country = "Polska",
                    PhoneNumber = "222333444",
                    City = "Wrocław",
                },

                // billing
                new PersonalInformation
                {
                    Id = 10,
                    FirstName = "Dominik",
                    LastName = "Marciniak",
                    StreetAddress = "Warszawska",
                    HouseNumber = "4",
                    ApartmentNumber = 3,
                    PostalCode = "50-041",
                    StateProvince = "dolnośląskie",
                    Country = "Polska",
                    PhoneNumber = "272332424",
                    City = "Wrocław",
                },

                // billing
                new PersonalInformation
                {
                    Id = 11,
                    FirstName = "Jacek",
                    LastName = "Marciniak",
                    StreetAddress = "Rynek",
                    HouseNumber = "12",
                    ApartmentNumber = 7,
                    PostalCode = "40-012",
                    StateProvince = "śląskie",
                    Country = "Polska",
                    PhoneNumber = "583549273",
                    City = "Katowice",
                },

                // billing
                new PersonalInformation
                {
                    Id = 12,
                    FirstName = "Amelia",
                    LastName = "Kołodziej",
                    StreetAddress = "Pływacka",
                    HouseNumber = "81",
                    PostalCode = "40-019",
                    StateProvince = "śląskie",
                    Country = "Polska",
                    PhoneNumber = "583729485",
                    City = "Katowice",
                },

                // dispatcher
                new PersonalInformation
                {
                    Id = 13,
                    FirstName = "Joanna",
                    LastName = "Nowak",
                    StreetAddress = "Górnicza",
                    HouseNumber = "18",
                    PostalCode = "40-003",
                    StateProvince = "śląskie",
                    Country = "Polska",
                    PhoneNumber = "239583745",
                    City = "Katowice",
                },

                // dispatcher
                new PersonalInformation
                {
                    Id = 14,
                    FirstName = "Grzegorz",
                    LastName = "Polański",
                    StreetAddress = "Różana",
                    HouseNumber = "1",
                    ApartmentNumber = 3,
                    PostalCode = "40-007",
                    StateProvince = "śląskie",
                    Country = "Polska",
                    PhoneNumber = "294725402",
                    City = "Katowice",
                },
            });
        }
    }
}
