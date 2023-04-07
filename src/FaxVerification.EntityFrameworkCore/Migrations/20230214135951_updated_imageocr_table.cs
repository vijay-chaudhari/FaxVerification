﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaxVerification.Migrations
{
    /// <inheritdoc />
    public partial class updatedimageocrtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppImageOcr");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppImageOcr");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppImageOcr");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppImageOcr");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppImageOcr");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppImageOcr");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppImageOcr");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppImageOcr");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppImageOcr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppImageOcr",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppImageOcr",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppImageOcr",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppImageOcr",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppImageOcr",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppImageOcr",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppImageOcr",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppImageOcr",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppImageOcr",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
