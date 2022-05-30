using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class PositionFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Positions_PositionId",
                table: "Resumes");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Positions_PositionId",
                table: "Resumes",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Positions_PositionId",
                table: "Resumes");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Positions_PositionId",
                table: "Resumes",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}
