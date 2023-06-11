using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
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
                value: new DateTime(2023, 6, 9, 14, 37, 29, 917, DateTimeKind.Local).AddTicks(9123));

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
