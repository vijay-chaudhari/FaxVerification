using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaxVerification.Migrations
{
    /// <inheritdoc />
    public partial class addednewcolumninputoutputpath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentName",
                table: "AppImageOcr",
                newName: "OutputPath");

            migrationBuilder.AddColumn<string>(
                name: "InputPath",
                table: "AppImageOcr",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputPath",
                table: "AppImageOcr");

            migrationBuilder.RenameColumn(
                name: "OutputPath",
                table: "AppImageOcr",
                newName: "DocumentName");
        }
    }
}
