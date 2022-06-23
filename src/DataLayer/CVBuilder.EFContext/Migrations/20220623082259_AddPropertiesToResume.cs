using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class AddPropertiesToResume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailabilityStatus",
                table: "Resumes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountDaysUnavailable",
                table: "Resumes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryRate",
                table: "Resumes",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncognito",
                table: "Proposal",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailabilityStatus",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "CountDaysUnavailable",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "SalaryRate",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "IsIncognito",
                table: "Proposal");
        }
    }
}
