using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class messagesRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MailboxType",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Messages");

            migrationBuilder.CreateTable(
                name: "MessageTransmissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailboxOwnerId = table.Column<long>(type: "bigint", nullable: true),
                    SenderId = table.Column<long>(type: "bigint", nullable: true),
                    RecipientId = table.Column<long>(type: "bigint", nullable: true),
                    MessageId = table.Column<long>(type: "bigint", nullable: true),
                    MailboxType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTransmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageTransmissions_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageTransmissions_Users_MailboxOwnerId",
                        column: x => x.MailboxOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageTransmissions_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageTransmissions_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageTransmissions_MailboxOwnerId",
                table: "MessageTransmissions",
                column: "MailboxOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTransmissions_MessageId",
                table: "MessageTransmissions",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTransmissions_RecipientId",
                table: "MessageTransmissions",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTransmissions_SenderId",
                table: "MessageTransmissions",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageTransmissions");

            migrationBuilder.AddColumn<int>(
                name: "MailboxType",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "RecipientId",
                table: "Messages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SenderId",
                table: "Messages",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
