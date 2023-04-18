using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class controlertest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalInfoId",
                table: "Patients",
                newName: "PersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "PersonalInfoId",
                table: "Paramedics",
                newName: "PersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "PersonalInfoId",
                table: "Dispatchers",
                newName: "PersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "PersonalInfoId",
                table: "Availabilities",
                newName: "PersonalInformationId");

            migrationBuilder.AlterColumn<int>(
                name: "BillingId",
                table: "Transports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalInformationId = table.Column<int>(type: "int", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Paramedics",
                columns: new[] { "Id", "BankAccountNumber", "IsDriver", "PersonalInformationId", "PhoneNumber" },
                values: new object[] { 1, "123123", true, 88, "123123123" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonalInformationId",
                value: 77);

            migrationBuilder.InsertData(
                table: "PersonalInformations",
                columns: new[] { "Id", "ApartmentNumber", "Country", "FirstName", "HouseNumber", "LastName", "PostalCode", "StateProvince", "StreetAddres" },
                values: new object[,]
                {
                    { 77, 1, "Polska", "Pan", 1, "Panowski", "41-100", "slask", "Kwiatowa" },
                    { 88, 4, "Polska", "Grzegorz", 3, "Kowalski", "42-400", "slask", "Stara" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Day", "DriverId", "ParamedicId" },
                values: new object[] { 1, new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "BillingId", "Destination", "Financing", "PatientId", "PatientPosition", "StartTime", "TeamId" },
                values: new object[] { 1, null, "Morawy", 0, 1, 0, new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), 1 });

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

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PersonalInformationId",
                table: "Patients",
                column: "PersonalInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PersonalInformations_PersonalInformationId",
                table: "Patients",
                column: "PersonalInformationId",
                principalTable: "PersonalInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Billings_BillingId",
                table: "Transports",
                column: "BillingId",
                principalTable: "Billings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Patients_PatientId",
                table: "Transports",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Teams_TeamId",
                table: "Transports",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PersonalInformations_PersonalInformationId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Billings_BillingId",
                table: "Transports");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Patients_PatientId",
                table: "Transports");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Teams_TeamId",
                table: "Transports");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropIndex(
                name: "IX_Transports_BillingId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_PatientId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_TeamId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PersonalInformationId",
                table: "Patients");

            migrationBuilder.DeleteData(
                table: "Paramedics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PersonalInformations",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "PersonalInformations",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "PersonalInformationId",
                table: "Patients",
                newName: "PersonalInfoId");

            migrationBuilder.RenameColumn(
                name: "PersonalInformationId",
                table: "Paramedics",
                newName: "PersonalInfoId");

            migrationBuilder.RenameColumn(
                name: "PersonalInformationId",
                table: "Dispatchers",
                newName: "PersonalInfoId");

            migrationBuilder.RenameColumn(
                name: "PersonalInformationId",
                table: "Availabilities",
                newName: "PersonalInfoId");

            migrationBuilder.AlterColumn<int>(
                name: "BillingId",
                table: "Transports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonalInfoId",
                value: 1);
        }
    }
}
