using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicDesk.App.Data.Migrations
{
    public partial class UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_RequesterId1",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "RequesterId1",
                table: "Requests",
                newName: "AssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_RequesterId1",
                table: "Requests",
                newName: "IX_Requests_AssignedToId");

            migrationBuilder.AlterColumn<string>(
                name: "RequesterId",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_GroupId",
                table: "Requests",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequesterId",
                table: "Requests",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_AssignedToId",
                table: "Requests",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Group_GroupId",
                table: "Requests",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_RequesterId",
                table: "Requests",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_AssignedToId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Group_GroupId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_RequesterId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Requests_GroupId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequesterId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "AssignedToId",
                table: "Requests",
                newName: "RequesterId1");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_AssignedToId",
                table: "Requests",
                newName: "IX_Requests_RequesterId1");

            migrationBuilder.AlterColumn<int>(
                name: "RequesterId",
                table: "Requests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_RequesterId1",
                table: "Requests",
                column: "RequesterId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
