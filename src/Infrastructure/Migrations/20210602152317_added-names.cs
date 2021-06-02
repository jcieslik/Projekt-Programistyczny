using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addednames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d5890c20-acc0-4e1f-890e-e98abc8a9873"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { new Guid("f561f3b5-d6c3-46cd-a10a-d7ccff63b697"), "example@example.com", true, "Jan", "admin", 1, "Kowalski", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f561f3b5-d6c3-46cd-a10a-d7ccff63b697"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Password", "Role", "Username" },
                values: new object[] { new Guid("d5890c20-acc0-4e1f-890e-e98abc8a9873"), "example@example.com", true, "admin", 1, "admin" });
        }
    }
}
