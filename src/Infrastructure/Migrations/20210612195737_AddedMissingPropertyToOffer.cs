using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddedMissingPropertyToOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46306a0d-a4d9-4a45-a32b-bd1f14e5e56d"));

            migrationBuilder.AddColumn<int>(
                name: "OfferType",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { new Guid("6e050a22-12d6-47b4-bddc-f6d99a5b1a60"), "example@example.com", true, "Jan", "admin", 1, "Kowalski", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6e050a22-12d6-47b4-bddc-f6d99a5b1a60"));

            migrationBuilder.DropColumn(
                name: "OfferType",
                table: "Offers");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { new Guid("46306a0d-a4d9-4a45-a32b-bd1f14e5e56d"), "example@example.com", true, "Jan", "admin", 1, "Kowalski", "admin" });
        }
    }
}
