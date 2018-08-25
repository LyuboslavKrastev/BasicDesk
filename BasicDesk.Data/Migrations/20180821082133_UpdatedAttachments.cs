using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicDesk.App.Data.Migrations
{
    public partial class UpdatedAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Attachments_AttachmentId",
                table: "Solutions");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_AttachmentId",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Solutions");

            migrationBuilder.CreateTable(
                name: "RequestAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PathToFile = table.Column<string>(nullable: true),
                    RequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestAttachments_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolutionAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PathToFile = table.Column<string>(nullable: true),
                    SolutionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolutionAttachments_Solutions_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "Solutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestAttachments_RequestId",
                table: "RequestAttachments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SolutionAttachments_SolutionId",
                table: "SolutionAttachments",
                column: "SolutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestAttachments");

            migrationBuilder.DropTable(
                name: "SolutionAttachments");

            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "Solutions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PathToFile = table.Column<string>(nullable: true),
                    RequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_AttachmentId",
                table: "Solutions",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_RequestId",
                table: "Attachments",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Attachments_AttachmentId",
                table: "Solutions",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
