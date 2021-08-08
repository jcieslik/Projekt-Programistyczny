using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class OrderWithComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Offers_OfferId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_OfferId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "WasCommented",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Comments");

            migrationBuilder.AddColumn<long>(
                name: "CommentId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CommentId",
                table: "Orders",
                column: "CommentId",
                unique: true,
                filter: "[CommentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Comments_CommentId",
                table: "Orders",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Comments_CommentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CommentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "WasCommented",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "OfferId",
                table: "Comments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OfferId",
                table: "Comments",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Offers_OfferId",
                table: "Comments",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
