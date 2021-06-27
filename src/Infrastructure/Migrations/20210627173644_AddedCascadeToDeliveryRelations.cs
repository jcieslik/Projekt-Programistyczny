using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddedCascadeToDeliveryRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersAndDeliveryMethods_DeliveryMethods_DeliveryMethodId",
                table: "OffersAndDeliveryMethods");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersAndDeliveryMethods_DeliveryMethods_DeliveryMethodId",
                table: "OffersAndDeliveryMethods",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersAndDeliveryMethods_DeliveryMethods_DeliveryMethodId",
                table: "OffersAndDeliveryMethods");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersAndDeliveryMethods_DeliveryMethods_DeliveryMethodId",
                table: "OffersAndDeliveryMethods",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
