﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.hesham.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class createImageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgName",
                table: "Employees");
        }
    }
}
