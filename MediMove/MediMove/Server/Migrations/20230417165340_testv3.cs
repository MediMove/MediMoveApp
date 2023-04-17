using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMove.Server.Migrations
{
    /// <inheritdoc />
    public partial class testv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Paramedic",
                table: "Paramedic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Availabilit",
                table: "Availabilit");

            migrationBuilder.RenameTable(
                name: "Paramedic",
                newName: "Paramedics");

            migrationBuilder.RenameTable(
                name: "Availabilit",
                newName: "Availabilities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paramedics",
                table: "Paramedics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Availabilities",
                table: "Availabilities",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Paramedics",
                table: "Paramedics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Availabilities",
                table: "Availabilities");

            migrationBuilder.RenameTable(
                name: "Paramedics",
                newName: "Paramedic");

            migrationBuilder.RenameTable(
                name: "Availabilities",
                newName: "Availabilit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paramedic",
                table: "Paramedic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Availabilit",
                table: "Availabilit",
                column: "Id");
        }
    }
}
