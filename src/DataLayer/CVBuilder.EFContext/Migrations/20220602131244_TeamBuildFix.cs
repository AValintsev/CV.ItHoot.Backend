using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class TeamBuildFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimationName",
                table: "TeamBuilds");

            migrationBuilder.AddColumn<int>(
                name: "Estimation",
                table: "TeamBuilds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estimation",
                table: "TeamBuilds");

            migrationBuilder.AddColumn<string>(
                name: "EstimationName",
                table: "TeamBuilds",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
