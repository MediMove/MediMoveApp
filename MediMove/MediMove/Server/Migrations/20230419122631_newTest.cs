using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class newTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Paramedics");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Dispatchers");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Dispatchers");

            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Availabilities");

            migrationBuilder.RenameColumn(
                name: "StreetAddres",
                table: "PersonalInformations",
                newName: "StreetAddress");

            migrationBuilder.RenameColumn(
                name: "PersonalInformationId",
                table: "Availabilities",
                newName: "ShiftType");

            migrationBuilder.RenameColumn(
                name: "InvoiceDate",
                table: "Availabilities",
                newName: "Day");

            migrationBuilder.AddColumn<int>(
                name: "TransportType",
                table: "Transports",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "PersonalInformations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PersonalInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PersonalInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParamedicId",
                table: "Availabilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Salary",
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
                    table.PrimaryKey("PK_Salary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salary_Dispatchers_DispatcherId",
                        column: x => x.DispatcherId,
                        principalTable: "Dispatchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "PersonalInformations",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "City", "HouseNumber", "PhoneNumber" },
                values: new object[] { "Krakow", "1", "123123123" });

            migrationBuilder.UpdateData(
                table: "PersonalInformations",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "City", "HouseNumber", "PhoneNumber" },
                values: new object[] { "Krakow", "3", "123123123" });

            migrationBuilder.InsertData(
                table: "PersonalInformations",
                columns: new[] { "Id", "ApartmentNumber", "City", "Country", "FirstName", "HouseNumber", "LastName", "PhoneNumber", "PostalCode", "StateProvince", "StreetAddress" },
                values: new object[] { 99, 5, "Katowice", "Polska", "Michal", "13", "Jakistam", "888888888", "42-800", "slask", "Sadowa" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Day", "DriverId" },
                values: new object[] { new DateTime(2023, 4, 19, 0, 0, 0, 0, DateTimeKind.Local), 2 });

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "StartTime", "TransportType" },
                values: new object[] { new DateTime(2023, 4, 19, 0, 0, 0, 0, DateTimeKind.Local), 0 });

            migrationBuilder.InsertData(
                table: "Paramedics",
                columns: new[] { "Id", "BankAccountNumber", "IsDriver", "PersonalInformationId" },
                values: new object[] { 2, "123123", true, 99 });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DriverId",
                table: "Teams",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ParamedicId",
                table: "Teams",
                column: "ParamedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_ParamedicId",
                table: "Rates",
                column: "ParamedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Paramedics_PersonalInformationId",
                table: "Paramedics",
                column: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchers_PersonalInformationId",
                table: "Dispatchers",
                column: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_PersonalInformationId",
                table: "Billings",
                column: "PersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_ParamedicId",
                table: "Availabilities",
                column: "ParamedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_DispatcherId",
                table: "Salary",
                column: "DispatcherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_Paramedics_ParamedicId",
                table: "Availabilities",
                column: "ParamedicId",
                principalTable: "Paramedics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Billings_PersonalInformations_PersonalInformationId",
                table: "Billings",
                column: "PersonalInformationId",
                principalTable: "PersonalInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dispatchers_PersonalInformations_PersonalInformationId",
                table: "Dispatchers",
                column: "PersonalInformationId",
                principalTable: "PersonalInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paramedics_PersonalInformations_PersonalInformationId",
                table: "Paramedics",
                column: "PersonalInformationId",
                principalTable: "PersonalInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Paramedics_ParamedicId",
                table: "Rates",
                column: "ParamedicId",
                principalTable: "Paramedics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Availabilities_Paramedics_ParamedicId",
                table: "Availabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Billings_PersonalInformations_PersonalInformationId",
                table: "Billings");

            migrationBuilder.DropForeignKey(
                name: "FK_Dispatchers_PersonalInformations_PersonalInformationId",
                table: "Dispatchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Paramedics_PersonalInformations_PersonalInformationId",
                table: "Paramedics");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Paramedics_ParamedicId",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Paramedics_DriverId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Paramedics_ParamedicId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropIndex(
                name: "IX_Teams_DriverId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ParamedicId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Rates_ParamedicId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Paramedics_PersonalInformationId",
                table: "Paramedics");

            migrationBuilder.DropIndex(
                name: "IX_Dispatchers_PersonalInformationId",
                table: "Dispatchers");

            migrationBuilder.DropIndex(
                name: "IX_Billings_PersonalInformationId",
                table: "Billings");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_ParamedicId",
                table: "Availabilities");

            migrationBuilder.DeleteData(
                table: "Paramedics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PersonalInformations",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DropColumn(
                name: "TransportType",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "City",
                table: "PersonalInformations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PersonalInformations");

            migrationBuilder.DropColumn(
                name: "ParamedicId",
                table: "Availabilities");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "PersonalInformations",
                newName: "StreetAddres");

            migrationBuilder.RenameColumn(
                name: "ShiftType",
                table: "Availabilities",
                newName: "PersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Availabilities",
                newName: "InvoiceDate");

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

            migrationBuilder.AlterColumn<int>(
                name: "HouseNumber",
                table: "PersonalInformations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Paramedics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Dispatchers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Dispatchers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountNumber",
                table: "Availabilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Availabilities",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Paramedics",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhoneNumber",
                value: "123123123");

            migrationBuilder.UpdateData(
                table: "PersonalInformations",
                keyColumn: "Id",
                keyValue: 77,
                column: "HouseNumber",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PersonalInformations",
                keyColumn: "Id",
                keyValue: 88,
                column: "HouseNumber",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Day", "DriverId" },
                values: new object[] { new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
