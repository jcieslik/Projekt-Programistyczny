using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class refactoredDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Brands_BrandId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Cities_CityId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Offers_BrandId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_CityId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Offers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "RateValue",
                table: "Comments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Comments",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "City", "PostCode", "Street" },
                values: new object[] { "Brak", "Brak", "Brak" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProvinceId",
                table: "Users",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SellerId",
                table: "Comments",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_SellerId",
                table: "Comments",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Provinces_ProvinceId",
                table: "Users",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_SellerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Provinces_ProvinceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProvinceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SellerId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RateValue",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Comments");

            migrationBuilder.AddColumn<long>(
                name: "BrandId",
                table: "Offers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Offers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    OfferId = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rates_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_BrandId",
                table: "Offers",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CityId",
                table: "Offers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CustomerId",
                table: "Rates",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_OfferId",
                table: "Rates",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Brands_BrandId",
                table: "Offers",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Cities_CityId",
                table: "Offers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
