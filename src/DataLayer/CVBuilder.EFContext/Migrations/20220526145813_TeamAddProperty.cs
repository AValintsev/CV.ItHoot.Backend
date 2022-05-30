using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class TeamAddProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowContacts",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowLogo",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowContacts",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ShowLogo",
                table: "Teams");
        }
    }
}
