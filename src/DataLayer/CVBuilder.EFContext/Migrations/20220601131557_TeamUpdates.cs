using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class TeamUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamBuildId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamBuildId",
                table: "Teams",
                column: "TeamBuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamBuilds_TeamBuildId",
                table: "Teams",
                column: "TeamBuildId",
                principalTable: "TeamBuilds",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamBuilds_TeamBuildId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamBuildId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamBuildId",
                table: "Teams");
        }
    }
}
