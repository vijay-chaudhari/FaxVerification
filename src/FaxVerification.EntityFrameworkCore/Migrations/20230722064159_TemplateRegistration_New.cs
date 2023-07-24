using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaxVerification.Migrations
{
    /// <inheritdoc />
    public partial class TemplateRegistration_New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registration",
                table: "Registration");

            migrationBuilder.RenameTable(
                name: "Registration",
                newName: "AppRegistration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRegistration",
                table: "AppRegistration",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRegistration",
                table: "AppRegistration");

            migrationBuilder.RenameTable(
                name: "AppRegistration",
                newName: "Registration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registration",
                table: "Registration",
                column: "Id");
        }
    }
}
