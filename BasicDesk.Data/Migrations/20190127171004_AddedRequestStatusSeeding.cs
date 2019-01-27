using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicDesk.App.Data.Migrations
{
    public partial class AddedRequestStatusSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RequestCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "Closed" },
                    { 3, "Rejected" },
                    { 4, "On Hold" },
                    { 5, "For Approval" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RequestCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequestCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RequestCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RequestCategories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
