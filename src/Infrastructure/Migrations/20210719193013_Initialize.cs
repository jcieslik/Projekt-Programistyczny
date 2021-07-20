using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageTransmissions_Users_RecipientId",
                table: "MessageTransmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageTransmissions_Users_SenderId",
                table: "MessageTransmissions");

            migrationBuilder.DropIndex(
                name: "IX_MessageTransmissions_RecipientId",
                table: "MessageTransmissions");

            migrationBuilder.DropIndex(
                name: "IX_MessageTransmissions_SenderId",
                table: "MessageTransmissions");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "MessageTransmissions");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "MessageTransmissions");

            migrationBuilder.AddColumn<long>(
                name: "SenderId",
                table: "Messages",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MessageUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<long>(type: "bigint", nullable: true),
                    RecipientId = table.Column<long>(type: "bigint", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageUser_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageUser_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageUser_MessageId",
                table: "MessageUser",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageUser_RecipientId",
                table: "MessageUser",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "MessageUser");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Messages");

            migrationBuilder.AddColumn<long>(
                name: "RecipientId",
                table: "MessageTransmissions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SenderId",
                table: "MessageTransmissions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageTransmissions_RecipientId",
                table: "MessageTransmissions",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTransmissions_SenderId",
                table: "MessageTransmissions",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTransmissions_Users_RecipientId",
                table: "MessageTransmissions",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageTransmissions_Users_SenderId",
                table: "MessageTransmissions",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
