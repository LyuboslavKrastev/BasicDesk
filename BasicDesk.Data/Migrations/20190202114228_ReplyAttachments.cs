using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicDesk.App.Data.Migrations
{
    public partial class ReplyAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestAttachments_RequestReplies_RequestReplyId",
                table: "RequestAttachments");

            migrationBuilder.DropIndex(
                name: "IX_RequestAttachments_RequestReplyId",
                table: "RequestAttachments");

            migrationBuilder.DropColumn(
                name: "RequestReplyId",
                table: "RequestAttachments");

            migrationBuilder.CreateTable(
                name: "ReplyAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 100, nullable: false),
                    PathToFile = table.Column<string>(maxLength: 400, nullable: false),
                    ReplyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyAttachments_RequestReplies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "RequestReplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplyAttachments_ReplyId",
                table: "ReplyAttachments",
                column: "ReplyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyAttachments");

            migrationBuilder.AddColumn<int>(
                name: "RequestReplyId",
                table: "RequestAttachments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestAttachments_RequestReplyId",
                table: "RequestAttachments",
                column: "RequestReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestAttachments_RequestReplies_RequestReplyId",
                table: "RequestAttachments",
                column: "RequestReplyId",
                principalTable: "RequestReplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
