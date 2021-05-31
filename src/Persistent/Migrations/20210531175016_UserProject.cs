using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistent.Migrations
{
    public partial class UserProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_Project_UserID",
                table: "UserProject");

            migrationBuilder.DropIndex(
                name: "IX_UserProject_UserID",
                table: "UserProject");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectID",
                table: "UserProject",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_Project_ProjectID",
                table: "UserProject",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_Project_ProjectID",
                table: "UserProject");

            migrationBuilder.DropIndex(
                name: "IX_UserProject_ProjectID",
                table: "UserProject");

            migrationBuilder.DropIndex(
                name: "IX_UserProject_UserID",
                table: "UserProject");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectID",
                table: "UserProject",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_UserID",
                table: "UserProject",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_Project_UserID",
                table: "UserProject",
                column: "UserID",
                principalTable: "Project",
                principalColumn: "ProjectID");
        }
    }
}
