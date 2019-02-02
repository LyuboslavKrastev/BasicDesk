using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicDesk.App.Data.Migrations
{
    public partial class AddedRequestReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestReplyId",
                table: "RequestAttachments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestReplies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    RequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestReplies_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestReplies_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestAttachments_RequestReplyId",
                table: "RequestAttachments",
                column: "RequestReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestReplies_AuthorId",
                table: "RequestReplies",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestReplies_RequestId",
                table: "RequestReplies",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestAttachments_RequestReplies_RequestReplyId",
                table: "RequestAttachments",
                column: "RequestReplyId",
                principalTable: "RequestReplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestAttachments_RequestReplies_RequestReplyId",
                table: "RequestAttachments");

            migrationBuilder.DropTable(
                name: "RequestReplies");

            migrationBuilder.DropIndex(
                name: "IX_RequestAttachments_RequestReplyId",
                table: "RequestAttachments");

            migrationBuilder.DropColumn(
                name: "RequestReplyId",
                table: "RequestAttachments");
        }
    }
}
