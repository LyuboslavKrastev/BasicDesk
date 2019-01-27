using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicDesk.App.Data.Migrations
{
    public partial class RequestApprovals_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "RequestApprovals",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "RequestApprovals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApprovalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApprovalStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Pending" });

            migrationBuilder.InsertData(
                table: "ApprovalStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Approved" });

            migrationBuilder.InsertData(
                table: "ApprovalStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Denied" });

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_StatusId",
                table: "RequestApprovals",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestApprovals_ApprovalStatuses_StatusId",
                table: "RequestApprovals",
                column: "StatusId",
                principalTable: "ApprovalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestApprovals_ApprovalStatuses_StatusId",
                table: "RequestApprovals");

            migrationBuilder.DropTable(
                name: "ApprovalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_RequestApprovals_StatusId",
                table: "RequestApprovals");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "RequestApprovals");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "RequestApprovals",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
