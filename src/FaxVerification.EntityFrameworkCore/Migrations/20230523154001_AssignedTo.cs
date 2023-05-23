using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaxVerification.Migrations
{
    /// <inheritdoc />
    public partial class AssignedTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignedTo",
                table: "AppImageOcr",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "AppImageOcr");
        }
    }
}
