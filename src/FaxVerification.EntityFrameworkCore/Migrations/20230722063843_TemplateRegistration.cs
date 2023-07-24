using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaxVerification.Migrations
{
    /// <inheritdoc />
    public partial class TemplateRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFieldConfig_AppConfigurationSettings_TemplateId",
                table: "AppFieldConfig");

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OCRConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppFieldConfig_AppConfigurationSettings_TemplateId",
                table: "AppFieldConfig",
                column: "TemplateId",
                principalTable: "AppConfigurationSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFieldConfig_AppConfigurationSettings_TemplateId",
                table: "AppFieldConfig");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFieldConfig_AppConfigurationSettings_TemplateId",
                table: "AppFieldConfig",
                column: "TemplateId",
                principalTable: "AppConfigurationSettings",
                principalColumn: "Id");
        }
    }
}
