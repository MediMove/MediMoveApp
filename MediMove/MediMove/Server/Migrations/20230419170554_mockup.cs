using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class mockup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "BillingId", "Destination", "Financing", "PatientId", "PatientPosition", "StartTime", "TeamId", "TransportType" },
                values: new object[,]
                {
                    { 2, null, "Katowice", 2, 1, 1, new DateTime(2023, 4, 19, 2, 0, 0, 0, DateTimeKind.Local), 1, 0 },
                    { 3, null, "Bytom", 1, 1, 2, new DateTime(2023, 4, 19, 5, 0, 0, 0, DateTimeKind.Local), 1, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
