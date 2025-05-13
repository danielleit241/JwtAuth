using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtAuth.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRefeshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefeshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefeshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefeshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefeshTokenExpiryTime",
                table: "Users");
        }
    }
}
