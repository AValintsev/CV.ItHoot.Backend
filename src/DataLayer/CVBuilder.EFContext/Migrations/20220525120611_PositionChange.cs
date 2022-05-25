using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class PositionChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "TeamResume");

            migrationBuilder.AddColumn<int>(
                name: "StatusResume",
                table: "TeamResume",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusResume",
                table: "TeamResume");

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "TeamResume",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
