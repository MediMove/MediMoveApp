using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class mockupv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2023, 4, 21, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2023, 4, 19, 2, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2023, 4, 19, 5, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
