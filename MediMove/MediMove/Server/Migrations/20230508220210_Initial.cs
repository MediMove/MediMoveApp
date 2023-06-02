using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PersonalInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billings_PersonalInformations_PersonalInformationId",
                        column: x => x.PersonalInformationId,
                        principalTable: "PersonalInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dispatchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dispatchers_PersonalInformations_PersonalInformationId",
                        column: x => x.PersonalInformationId,
                        principalTable: "PersonalInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DispatcherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_Dispatchers_DispatcherId",
                        column: x => x.DispatcherId,
                        principalTable: "Dispatchers",
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
                        name: "FK_Transports_Billings_BillingId",
                        column: x => x.BillingId,
                        principalTable: "Billings",
                        principalColumn: "Id");
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
                    { 1, null, "Warszawa", "Polska", "Adam", "10", "Nowak", "123456789", "00-001", "mazowieckie", "Kwiatowa" },
                    { 2, null, "Warszawa", "Polska", "Ewa", "20", "Kowalska", "987654321", "02-002", "mazowieckie", "Koszykowa" },
                    { 3, 1, "Warszawa", "Polska", "Piotr", "30", "Lewandowski", "555444333", "03-003", "mazowieckie", "Wrocławska" },
                    { 4, 2, "Poznań", "Polska", "Magdalena", "40", "Kaczmarek", "111222333", "04-004", "wielkopolskie", "Piłsudskiego" },
                    { 5, null, "Kraków", "Polska", "Marek", "50", "Jankowski", "555888999", "05-005", "małopolskie", "Krakowska" },
                    { 6, 7, "Gdańsk", "Polska", "Anna", "12", "Kowalczyk", "789232737", "80-255", "pomorskie", "Polna" },
                    { 7, 2, "Warszawa", "Polska", "Marek", "34", "Michalski", "444555666", "00-207", "mazowieckie", "Jana Pawła II" },
                    { 8, 5, "Katowice", "Polska", "Magdalena", "17", "Jankowska", "777888999", "40-009", "śląskie", "Mickiewicza" },
                    { 9, 12, "Wrocław", "Polska", "Paweł", "7", "Wójcik", "222333444", "50-049", "dolnośląskie", "Słowackiego" },
                    { 10, 3, "Wrocław", "Polska", "Dominik", "4", "Marciniak", "272332424", "50-041", "dolnośląskie", "Warszawska" },
                    { 11, 7, "Katowice", "Polska", "Jacek", "12", "Marciniak", "583549273", "40-012", "śląskie", "Rynek" },
                    { 12, null, "Katowice", "Polska", "Amelia", "81", "Kołodziej", "583729485", "40-019", "śląskie", "Pływacka" },
                    { 13, null, "Katowice", "Polska", "Joanna", "18", "Nowak", "239583745", "40-003", "śląskie", "Górnicza" },
                    { 14, 3, "Katowice", "Polska", "Grzegorz", "1", "Polański", "294725402", "40-007", "śląskie", "Różana" },
                    { 15, null, "Kraków", "Polska", "Marek", "50", "Jankowski", "555888999", "05-005", "małopolskie", "Krakowska" }
                });

            migrationBuilder.InsertData(
                table: "Billings",
                columns: new[] { "Id", "BankAccountNumber", "Cost", "InvoiceDate", "InvoiceNumber", "PersonalInformationId" },
                values: new object[,]
                {
                    { 1, "342301332136124", 200m, new DateTime(2023, 5, 7, 0, 2, 10, 168, DateTimeKind.Local).AddTicks(6422), "1.09.05.2023", 10 },
                    { 2, "343301232434821", 90m, new DateTime(2023, 5, 7, 23, 59, 0, 0, DateTimeKind.Local), "2.09.05.2023", 11 },
                    { 3, "543322635238421", 50m, new DateTime(2023, 5, 8, 0, 1, 0, 0, DateTimeKind.Local), "3.09.05.2023", 12 }
                });

            migrationBuilder.InsertData(
                table: "Dispatchers",
                columns: new[] { "Id", "BankAccountNumber", "PersonalInformationId" },
                values: new object[,]
                {
                    { 1, "4203987928122474", 13 },
                    { 2, "4203787958122274", 14 }
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
                    { 5, "982301231238812", true, 5 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "PersonalInformationId", "Weight" },
                values: new object[,]
                {
                    { 1, 6, 60 },
                    { 2, 7, 78 },
                    { 3, 8, 55 },
                    { 4, 9, 93 },
                    { 5, 15, 100 }
                });

            migrationBuilder.InsertData(
                table: "Availabilities",
                columns: new[] { "Id", "Day", "ParamedicId", "ShiftType" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 1, 0 },
                    { 2, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 3, 0 },
                    { 3, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 4, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 5, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 1, 0 },
                    { 6, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 2, 0 },
                    { 7, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 8, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 4, 0 },
                    { 9, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 10, new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, 0 },
                    { 11, new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), 2, 0 },
                    { 12, new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), 3, 0 },
                    { 13, new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 14, new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 15, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 1, 0 },
                    { 16, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 3, 0 },
                    { 17, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 18, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 19, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), 1, 0 },
                    { 20, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), 2, 1 },
                    { 21, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 22, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), 4, 0 },
                    { 23, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Date", "ParamedicId", "PayPerHour" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 1, 12m },
                    { 2, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 2, 12m },
                    { 3, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 3, 12m },
                    { 4, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 4, 12m },
                    { 5, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 5, 12m },
                    { 6, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 1, 13m },
                    { 7, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 5, 13m },
                    { 8, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 1, 14m }
                });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "Id", "Date", "DispatcherId", "Income" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 1, 1200m },
                    { 2, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 2, 1200m },
                    { 3, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 1, 1300m },
                    { 4, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 1, 1500m }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Day", "DriverId", "ParamedicId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 2, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 3, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 4, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 5, 3 },
                    { 5, new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 6, new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 7, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 8, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 9, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 10, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "BillingId", "Destination", "Financing", "PatientId", "PatientPosition", "StartTime", "TeamId", "TransportType" },
                values: new object[,]
                {
                    { 1, null, "Saska 43 Rybnik", 0, 1, 0, new DateTime(2023, 5, 7, 7, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 },
                    { 2, null, "Nadrzeczna 55 Mysłowice", 2, 2, 1, new DateTime(2023, 5, 7, 9, 10, 0, 0, DateTimeKind.Unspecified), 1, 0 },
                    { 3, null, "Wyszogrodzka 44 Bytom", 1, 3, 1, new DateTime(2023, 5, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 4, null, "Dobosza 101 Zabrze", 1, 4, 1, new DateTime(2023, 5, 7, 7, 30, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 5, null, "Dyngus 30 Chorzów", 0, 5, 2, new DateTime(2023, 5, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 0 },
                    { 6, null, "Obornicka 89 Rybnik", 0, 4, 1, new DateTime(2023, 5, 8, 7, 30, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 7, null, "Zakładowa 19 Częstochowa", 0, 2, 0, new DateTime(2023, 5, 8, 9, 45, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 8, null, "Ustronna 70 Gliwice", 0, 1, 0, new DateTime(2023, 5, 8, 13, 0, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 9, null, "Oboźna 43 Tychy", 0, 4, 1, new DateTime(2023, 5, 8, 15, 30, 0, 0, DateTimeKind.Unspecified), 4, 0 },
                    { 10, null, "Mickiewicza 119 Żory", 0, 2, 0, new DateTime(2023, 5, 8, 18, 0, 0, 0, DateTimeKind.Unspecified), 4, 0 },
                    { 11, null, "Rycerska 13 Sosnowiec", 0, 5, 2, new DateTime(2023, 5, 8, 20, 0, 0, 0, DateTimeKind.Unspecified), 4, 0 },
                    { 12, null, "Skromna 116 Bieruń", 0, 1, 0, new DateTime(2023, 5, 9, 7, 0, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 13, null, "Lidzka 53 Świętochłowice", 0, 3, 0, new DateTime(2023, 5, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 14, null, "Pomarańczowa 95 Bielsko-Biała", 0, 2, 0, new DateTime(2023, 5, 9, 13, 30, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 15, null, "Bydgoska 123 Ruda Śląska", 0, 4, 1, new DateTime(2023, 5, 9, 16, 0, 0, 0, DateTimeKind.Unspecified), 6, 1 },
                    { 16, null, "Różana 138 Żory", 0, 2, 0, new DateTime(2023, 5, 9, 17, 45, 0, 0, DateTimeKind.Unspecified), 6, 1 },
                    { 17, null, "Okrzei Stefana 132 Chorzów", 0, 4, 1, new DateTime(2023, 5, 9, 19, 0, 0, 0, DateTimeKind.Unspecified), 6, 0 },
                    { 18, null, "Diamentowa 5 Częstochowa", 0, 1, 0, new DateTime(2023, 5, 10, 7, 15, 0, 0, DateTimeKind.Unspecified), 7, 1 },
                    { 19, null, "Pawlikowskiego Tadeusza 96 Cieszyn", 0, 2, 0, new DateTime(2023, 5, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 7, 0 },
                    { 20, null, "Generała Szyllinga Antoniego 111 Imielin", 0, 3, 0, new DateTime(2023, 5, 10, 13, 15, 0, 0, DateTimeKind.Unspecified), 7, 1 },
                    { 21, null, "Urbańskiego Tadeusza 45 Pszów", 0, 5, 2, new DateTime(2023, 5, 10, 16, 0, 0, 0, DateTimeKind.Unspecified), 8, 1 },
                    { 22, null, "Czerwona 46 Katowice", 0, 4, 1, new DateTime(2023, 5, 10, 17, 15, 0, 0, DateTimeKind.Unspecified), 8, 1 },
                    { 23, null, "Reutta 54 Bytom", 0, 3, 0, new DateTime(2023, 5, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), 8, 0 },
                    { 24, null, "Cybernetyków 120 Tychy", 0, 1, 0, new DateTime(2023, 5, 11, 8, 30, 0, 0, DateTimeKind.Unspecified), 9, 1 },
                    { 25, null, "Kaszubska 18 Świętochłowice", 0, 3, 0, new DateTime(2023, 5, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 9, 1 },
                    { 26, null, "Cybernetyków 120 Tychy", 0, 2, 0, new DateTime(2023, 5, 11, 17, 0, 0, 0, DateTimeKind.Unspecified), 10, 1 },
                    { 27, null, "Kaszubska 18 Świętochłowice", 0, 5, 2, new DateTime(2023, 5, 11, 19, 0, 0, 0, DateTimeKind.Unspecified), 10, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_ParamedicId",
                table: "Availabilities",
                column: "ParamedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_PersonalInformationId",
                table: "Billings",
                column: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchers_PersonalInformationId",
                table: "Dispatchers",
                column: "PersonalInformationId");

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
                name: "IX_Salaries_DispatcherId",
                table: "Salaries",
                column: "DispatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DriverId",
                table: "Teams",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ParamedicId",
                table: "Teams",
                column: "ParamedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_BillingId",
                table: "Transports",
                column: "BillingId");

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
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Dispatchers");

            migrationBuilder.DropTable(
                name: "Billings");

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
