using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class NewModelTestv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonalInformations",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Paramedics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PersonalInformations",
                keyColumn: "Id",
                keyValue: 88);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PersonalInformations",
                columns: new[] { "Id", "ApartmentNumber", "City", "Country", "FirstName", "HouseNumber", "LastName", "PhoneNumber", "PostalCode", "StateProvince", "StreetAddress" },
                values: new object[,]
                {
                    { 77, 1, "Krakow", "Polska", "Pan", "1", "Panowski", "123123123", "41-100", "slask", "Kwiatowa" },
                    { 88, 4, "Krakow", "Polska", "Grzegorz", "3", "Kowalski", "123123123", "42-400", "slask", "Stara" }
                });

            migrationBuilder.InsertData(
                table: "Paramedics",
                columns: new[] { "Id", "BankAccountNumber", "IsDriver", "PersonalInformationId" },
                values: new object[] { 1, "123123", true, 88 });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Day", "DriverId", "ParamedicId" },
                values: new object[] { 1, new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "BillingId", "Destination", "Financing", "PatientId", "PatientPosition", "StartTime", "TeamId", "TransportType" },
                values: new object[] { 1, null, "Morawy", 0, 1, 0, new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), 1, 0 });
        }
    }
}
