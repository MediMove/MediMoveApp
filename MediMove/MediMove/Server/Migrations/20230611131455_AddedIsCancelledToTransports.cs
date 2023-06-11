using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsCancelledToTransports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Paramedics_DriverId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Paramedics_ParamedicId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Teams_TeamId",
                table: "Transports");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Transports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Transports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ParamedicId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Billings",
                keyColumn: "Id",
                keyValue: 1,
                column: "InvoiceDate",
                value: new DateTime(2023, 6, 9, 15, 14, 54, 789, DateTimeKind.Local).AddTicks(7519));

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 14,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 15,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 16,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 17,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 18,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 19,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 20,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 21,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 22,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 23,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 24,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 25,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 26,
                column: "IsCancelled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 27,
                column: "IsCancelled",
                value: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Paramedics_DriverId",
                table: "Teams",
                column: "DriverId",
                principalTable: "Paramedics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Paramedics_ParamedicId",
                table: "Teams",
                column: "ParamedicId",
                principalTable: "Paramedics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Teams_TeamId",
                table: "Transports",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Paramedics_DriverId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Paramedics_ParamedicId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Teams_TeamId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Transports");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Transports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParamedicId",
                table: "Teams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Teams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Billings",
                keyColumn: "Id",
                keyValue: 1,
                column: "InvoiceDate",
                value: new DateTime(2023, 6, 9, 14, 42, 37, 925, DateTimeKind.Local).AddTicks(8684));

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Paramedics_DriverId",
                table: "Teams",
                column: "DriverId",
                principalTable: "Paramedics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Paramedics_ParamedicId",
                table: "Teams",
                column: "ParamedicId",
                principalTable: "Paramedics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Teams_TeamId",
                table: "Transports",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
