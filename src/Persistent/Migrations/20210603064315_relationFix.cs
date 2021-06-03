using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistent.Migrations
{
    public partial class relationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProject_ProjectID",
                table: "UserProject");

            migrationBuilder.DropIndex(
                name: "IX_UserProject_UserID",
                table: "UserProject");

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    ProjectsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.ProjectsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_ProjectID",
                table: "UserProject",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_UserID",
                table: "UserProject",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_UsersId",
                table: "ProjectUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");

            migrationBuilder.DropIndex(
                name: "IX_UserProject_ProjectID",
                table: "UserProject");

            migrationBuilder.DropIndex(
                name: "IX_UserProject_UserID",
                table: "UserProject");

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_ProjectID",
                table: "UserProject",
                column: "ProjectID",
                unique: true,
                filter: "[ProjectID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_UserID",
                table: "UserProject",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");
        }
    }
}
