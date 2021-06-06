using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("65056649-b59c-4e52-a779-547fe6f12360"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { new Guid("c4ad03a9-ca9b-4888-b856-b1f9b1583b10"), "example@example.com", true, "Jan", "admin", 1, "Kowalski", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4ad03a9-ca9b-4888-b856-b1f9b1583b10"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { new Guid("65056649-b59c-4e52-a779-547fe6f12360"), "example@example.com", true, "Jan", "admin", 1, "Kowalski", "admin" });
        }
    }
}
