using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CartAndOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartOffer_Carts_CartsId",
                table: "CartOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_CartOffer_Offers_OffersId",
                table: "CartOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartOffer",
                table: "CartOffer");

            migrationBuilder.DropIndex(
                name: "IX_CartOffer_OffersId",
                table: "CartOffer");

            migrationBuilder.RenameColumn(
                name: "FullPrice",
                table: "OffersAndDeliveryMethods",
                newName: "DeliveryFullPrice");

            migrationBuilder.RenameColumn(
                name: "OffersId",
                table: "CartOffer",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CartsId",
                table: "CartOffer",
                newName: "CreatedBy");

            migrationBuilder.AddColumn<long>(
                name: "OfferWithDeliveryId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MinimalBid",
                table: "Offers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "CartOffer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "CartId",
                table: "CartOffer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CartOffer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "CartOffer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OfferId",
                table: "CartOffer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsCount",
                table: "CartOffer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartOffer",
                table: "CartOffer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OfferWithDeliveryId",
                table: "Orders",
                column: "OfferWithDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartOffer_CartId",
                table: "CartOffer",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartOffer_OfferId",
                table: "CartOffer",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartOffer_Carts_CartId",
                table: "CartOffer",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartOffer_Offers_OfferId",
                table: "CartOffer",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OffersAndDeliveryMethods_OfferWithDeliveryId",
                table: "Orders",
                column: "OfferWithDeliveryId",
                principalTable: "OffersAndDeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartOffer_Carts_CartId",
                table: "CartOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_CartOffer_Offers_OfferId",
                table: "CartOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OffersAndDeliveryMethods_OfferWithDeliveryId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OfferWithDeliveryId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartOffer",
                table: "CartOffer");

            migrationBuilder.DropIndex(
                name: "IX_CartOffer_CartId",
                table: "CartOffer");

            migrationBuilder.DropIndex(
                name: "IX_CartOffer_OfferId",
                table: "CartOffer");

            migrationBuilder.DropColumn(
                name: "OfferWithDeliveryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MinimalBid",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CartOffer");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "CartOffer");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CartOffer");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "CartOffer");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "CartOffer");

            migrationBuilder.DropColumn(
                name: "ProductsCount",
                table: "CartOffer");

            migrationBuilder.RenameColumn(
                name: "DeliveryFullPrice",
                table: "OffersAndDeliveryMethods",
                newName: "FullPrice");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "CartOffer",
                newName: "OffersId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CartOffer",
                newName: "CartsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartOffer",
                table: "CartOffer",
                columns: new[] { "CartsId", "OffersId" });

            migrationBuilder.CreateIndex(
                name: "IX_CartOffer_OffersId",
                table: "CartOffer",
                column: "OffersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartOffer_Carts_CartsId",
                table: "CartOffer",
                column: "CartsId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartOffer_Offers_OffersId",
                table: "CartOffer",
                column: "OffersId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
