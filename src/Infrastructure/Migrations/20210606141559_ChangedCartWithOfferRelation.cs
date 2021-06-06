using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangedCartWithOfferRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartsAndOffers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9ca7a69f-3805-42dc-95a1-2941d3397b23"));

            migrationBuilder.CreateTable(
                name: "CartOffer",
                columns: table => new
                {
                    CartsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OffersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartOffer", x => new { x.CartsId, x.OffersId });
                    table.ForeignKey(
                        name: "FK_CartOffer_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartOffer_Offers_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { new Guid("65056649-b59c-4e52-a779-547fe6f12360"), "example@example.com", true, "Jan", "admin", 1, "Kowalski", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_CartOffer_OffersId",
                table: "CartOffer",
                column: "OffersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartOffer");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("65056649-b59c-4e52-a779-547fe6f12360"));

            migrationBuilder.CreateTable(
                name: "CartsAndOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartsAndOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartsAndOffers_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartsAndOffers_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[] { new Guid("9ca7a69f-3805-42dc-95a1-2941d3397b23"), "example@example.com", true, "Jan", "admin", 1, "Kowalski", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_CartsAndOffers_CartId",
                table: "CartsAndOffers",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartsAndOffers_OfferId",
                table: "CartsAndOffers",
                column: "OfferId");
        }
    }
}
