using Microsoft.EntityFrameworkCore.Migrations;

namespace CVBuilder.EFContext.Migrations
{
    public partial class AddCvNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "CVs",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CVs",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "CvName",
                table: "CVs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvName",
                table: "CVs");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "CVs",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "CVs",
                newName: "Name");
        }
    }
}
