using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicDesk.App.Data.Migrations
{
    public partial class AddedRequestApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RequestId = table.Column<int>(nullable: false),
                    RequesterId = table.Column<string>(nullable: true),
                    ApproverId = table.Column<string>(nullable: true),
                    ApproverComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestApprovals_AspNetUsers_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestApprovals_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestApprovals_AspNetUsers_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_ApproverId",
                table: "RequestApprovals",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_RequestId",
                table: "RequestApprovals",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_RequesterId",
                table: "RequestApprovals",
                column: "RequesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestApprovals");
        }
    }
}
