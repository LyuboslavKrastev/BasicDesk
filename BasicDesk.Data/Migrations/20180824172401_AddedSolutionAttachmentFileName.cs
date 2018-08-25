using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicDesk.App.Data.Migrations
{
    public partial class AddedSolutionAttachmentFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "SolutionAttachments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "SolutionAttachments");
        }
    }
}
