using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddedIsHiddenToSomeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4ad03a9-ca9b-4888-b856-b1f9b1583b10"));

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Wishes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Bids",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { new Guid("46306a0d-a4d9-4a45-a32b-bd1f14e5e56d"), "example@example.com", true, "Jan", "admin", 1, "Kowalski", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46306a0d-a4d9-4a45-a32b-bd1f14e5e56d"));

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Wishes");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Bids");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { new Guid("c4ad03a9-ca9b-4888-b856-b1f9b1583b10"), "example@example.com", true, "Jan", "admin", 1, "Kowalski", "admin" });
        }
    }
}
