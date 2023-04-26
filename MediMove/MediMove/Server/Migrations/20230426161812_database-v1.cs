using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class databasev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApartmentNumber = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paramedics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDriver = table.Column<bool>(type: "bit", nullable: false),
                    PersonalInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paramedics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paramedics_PersonalInformations_PersonalInformationId",
                        column: x => x.PersonalInformationId,
                        principalTable: "PersonalInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    PersonalInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_PersonalInformations_PersonalInformationId",
                        column: x => x.PersonalInformationId,
                        principalTable: "PersonalInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    ParamedicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availabilities_Paramedics_ParamedicId",
                        column: x => x.ParamedicId,
                        principalTable: "Paramedics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayPerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ParamedicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Paramedics_ParamedicId",
                        column: x => x.ParamedicId,
                        principalTable: "Paramedics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    ParamedicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Paramedics_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Paramedics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_Paramedics_ParamedicId",
                        column: x => x.ParamedicId,
                        principalTable: "Paramedics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientPosition = table.Column<int>(type: "int", nullable: false),
                    Financing = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportType = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    BillingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transports_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "PersonalInformations",
                columns: new[] { "Id", "ApartmentNumber", "City", "Country", "FirstName", "HouseNumber", "LastName", "PhoneNumber", "PostalCode", "StateProvince", "StreetAddress" },
                values: new object[,]
                {
                    { 1, 1, "Krakow", "Polska", "Pan", "1", "Panowski", "645839002", "41-100", "śląsk", "Kwiatowa" },
                    { 2, 5, "Katowice", "Polska", "Michal", "13", "Jakistam", "854888145", "42-800", "śląsk", "Sadowa" },
                    { 3, 4, "Krakow", "Polska", "Grzegorz", "3", "Kowalski", "123123123", "42-400", "wielkopolskie", "Stara" },
                    { 4, null, "Krakow", "Polska", "Marek", "44", "Zygmunt", "888888888", "23-213", "wielkopolskie", "Kwiatowa" },
                    { 5, null, "Kraków", "Polska", "Kamil", "10", "Nowak", "555777888", "30-062", "małopolskie", "Rynek" },
                    { 6, 7, "Gdańsk", "Polska", "Anna", "12", "Kowalczyk", "789232737", "80-850", "pomorskie", "Polna" },
                    { 7, 2, "Warszawa", "Polska", "Marek", "34", "Michalski", "444555666", "01-001", "mazowieckie", "Jana Pawła II" },
                    { 8, 5, "Katowice", "Polska", "Magdalena", "17", "Jankowska", "777888999", "40-005", "śląskie", "Mickiewicza" },
                    { 9, 12, "Wrocław", "Polska", "Paweł", "7", "Wójcik", "222333444", "50-049", "dolnośląskie", "Słowackiego" }
                });

            migrationBuilder.InsertData(
                table: "Paramedics",
                columns: new[] { "Id", "BankAccountNumber", "IsDriver", "PersonalInformationId" },
                values: new object[,]
                {
                    { 1, "1203987908127474", true, 1 },
                    { 2, "124341763465609", true, 2 },
                    { 3, "121234124123109", false, 3 },
                    { 4, "123456780123109", false, 4 },
                    { 5, "982301231238812", true, 3 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "PersonalInformationId", "Weight" },
                values: new object[,]
                {
                    { 1, 6, 60 },
                    { 2, 7, 78 },
                    { 3, 8, 55 },
                    { 4, 9, 93 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Day", "DriverId", "ParamedicId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), 2, 3 },
                    { 2, new DateTime(2023, 4, 27, 0, 0, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 3, new DateTime(2023, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "BillingId", "Destination", "Financing", "PatientId", "PatientPosition", "StartTime", "TeamId", "TransportType" },
                values: new object[,]
                {
                    { 1, null, "Rybnik", 0, 1, 0, new DateTime(2023, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), 1, 0 },
                    { 2, null, "Mysłowice", 2, 2, 1, new DateTime(2023, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), 1, 0 },
                    { 3, null, "Bytom", 1, 3, 1, new DateTime(2023, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 4, null, "Zabrze", 1, 1, 0, new DateTime(2023, 4, 27, 0, 0, 0, 0, DateTimeKind.Local), 2, 1 },
                    { 5, null, "Chorzów", 0, 4, 0, new DateTime(2023, 4, 27, 0, 0, 0, 0, DateTimeKind.Local), 2, 0 },
                    { 6, null, "Rybnik", 0, 4, 0, new DateTime(2023, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), 3, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_ParamedicId",
                table: "Availabilities",
                column: "ParamedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Paramedics_PersonalInformationId",
                table: "Paramedics",
                column: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PersonalInformationId",
                table: "Patients",
                column: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_ParamedicId",
                table: "Rates",
                column: "ParamedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DriverId",
                table: "Teams",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ParamedicId",
                table: "Teams",
                column: "ParamedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_PatientId",
                table: "Transports",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_TeamId",
                table: "Transports",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Paramedics");

            migrationBuilder.DropTable(
                name: "PersonalInformations");
        }
    }
}
