using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthModelsWithRoleSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 8,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 10,
                column: "Day",
                value: new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 11,
                column: "Day",
                value: new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 12,
                column: "Day",
                value: new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 13,
                column: "Day",
                value: new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 14,
                column: "Day",
                value: new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 15,
                column: "Day",
                value: new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 16,
                column: "Day",
                value: new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 17,
                column: "Day",
                value: new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 18,
                column: "Day",
                value: new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 19,
                column: "Day",
                value: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 20,
                column: "Day",
                value: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 21,
                column: "Day",
                value: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 22,
                column: "Day",
                value: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 23,
                column: "Day",
                value: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Billings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InvoiceDate", "InvoiceNumber" },
                values: new object[] { new DateTime(2023, 5, 10, 13, 57, 27, 438, DateTimeKind.Local).AddTicks(7296), "1.12.05.2023" });

            migrationBuilder.UpdateData(
                table: "Billings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InvoiceDate", "InvoiceNumber" },
                values: new object[] { new DateTime(2023, 5, 10, 23, 59, 0, 0, DateTimeKind.Local), "2.12.05.2023" });

            migrationBuilder.UpdateData(
                table: "Billings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "InvoiceDate", "InvoiceNumber" },
                values: new object[] { new DateTime(2023, 5, 11, 0, 1, 0, 0, DateTimeKind.Local), "3.12.05.2023" });

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Undefined" },
                    { 2, "Paramedic" },
                    { 3, "Dispatcher" },
                    { 4, "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5,
                column: "Day",
                value: new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6,
                column: "Day",
                value: new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7,
                column: "Day",
                value: new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 8,
                column: "Day",
                value: new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 9,
                column: "Day",
                value: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 10,
                column: "Day",
                value: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 7, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 9, 10, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 7, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 7, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 7,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 9, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 8,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 13, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 9,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 15, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 10,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 18, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 11,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 20, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 12,
                column: "StartTime",
                value: new DateTime(2023, 5, 12, 7, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 13,
                column: "StartTime",
                value: new DateTime(2023, 5, 12, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 14,
                column: "StartTime",
                value: new DateTime(2023, 5, 12, 13, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 15,
                column: "StartTime",
                value: new DateTime(2023, 5, 12, 16, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 16,
                column: "StartTime",
                value: new DateTime(2023, 5, 12, 17, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 17,
                column: "StartTime",
                value: new DateTime(2023, 5, 12, 19, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 18,
                column: "StartTime",
                value: new DateTime(2023, 5, 13, 7, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 19,
                column: "StartTime",
                value: new DateTime(2023, 5, 13, 11, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 20,
                column: "StartTime",
                value: new DateTime(2023, 5, 13, 13, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 21,
                column: "StartTime",
                value: new DateTime(2023, 5, 13, 16, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 22,
                column: "StartTime",
                value: new DateTime(2023, 5, 13, 17, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 23,
                column: "StartTime",
                value: new DateTime(2023, 5, 13, 20, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 24,
                column: "StartTime",
                value: new DateTime(2023, 5, 14, 8, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 25,
                column: "StartTime",
                value: new DateTime(2023, 5, 14, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 26,
                column: "StartTime",
                value: new DateTime(2023, 5, 14, 17, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 27,
                column: "StartTime",
                value: new DateTime(2023, 5, 14, 19, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Day",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Day",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Day",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Day",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Day",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Day",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Day",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 8,
                column: "Day",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Day",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 10,
                column: "Day",
                value: new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 11,
                column: "Day",
                value: new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 12,
                column: "Day",
                value: new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 13,
                column: "Day",
                value: new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 14,
                column: "Day",
                value: new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 15,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 16,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 17,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 18,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 19,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 20,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 21,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 22,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 23,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Billings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InvoiceDate", "InvoiceNumber" },
                values: new object[] { new DateTime(2023, 5, 7, 0, 2, 10, 168, DateTimeKind.Local).AddTicks(6422), "1.09.05.2023" });

            migrationBuilder.UpdateData(
                table: "Billings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InvoiceDate", "InvoiceNumber" },
                values: new object[] { new DateTime(2023, 5, 7, 23, 59, 0, 0, DateTimeKind.Local), "2.09.05.2023" });

            migrationBuilder.UpdateData(
                table: "Billings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "InvoiceDate", "InvoiceNumber" },
                values: new object[] { new DateTime(2023, 5, 8, 0, 1, 0, 0, DateTimeKind.Local), "3.09.05.2023" });

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "Day",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "Day",
                value: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                column: "Day",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4,
                column: "Day",
                value: new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5,
                column: "Day",
                value: new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6,
                column: "Day",
                value: new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 8,
                column: "Day",
                value: new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 9,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 10,
                column: "Day",
                value: new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2023, 5, 7, 7, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2023, 5, 7, 9, 10, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2023, 5, 7, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2023, 5, 7, 7, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartTime",
                value: new DateTime(2023, 5, 7, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartTime",
                value: new DateTime(2023, 5, 8, 7, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 7,
                column: "StartTime",
                value: new DateTime(2023, 5, 8, 9, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 8,
                column: "StartTime",
                value: new DateTime(2023, 5, 8, 13, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 9,
                column: "StartTime",
                value: new DateTime(2023, 5, 8, 15, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 10,
                column: "StartTime",
                value: new DateTime(2023, 5, 8, 18, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 11,
                column: "StartTime",
                value: new DateTime(2023, 5, 8, 20, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 12,
                column: "StartTime",
                value: new DateTime(2023, 5, 9, 7, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 13,
                column: "StartTime",
                value: new DateTime(2023, 5, 9, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 14,
                column: "StartTime",
                value: new DateTime(2023, 5, 9, 13, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 15,
                column: "StartTime",
                value: new DateTime(2023, 5, 9, 16, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 16,
                column: "StartTime",
                value: new DateTime(2023, 5, 9, 17, 45, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 17,
                column: "StartTime",
                value: new DateTime(2023, 5, 9, 19, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 18,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 7, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 19,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 11, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 20,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 13, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 21,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 16, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 22,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 17, 15, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 23,
                column: "StartTime",
                value: new DateTime(2023, 5, 10, 20, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 24,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 8, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 25,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 26,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 17, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 27,
                column: "StartTime",
                value: new DateTime(2023, 5, 11, 19, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
