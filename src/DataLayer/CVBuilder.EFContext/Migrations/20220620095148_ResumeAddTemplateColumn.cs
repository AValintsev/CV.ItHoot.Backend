using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class ResumeAddTemplateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResumeTemplateId",
                table: "Resumes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_ResumeTemplateId",
                table: "Resumes",
                column: "ResumeTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_ResumeTemplate_ResumeTemplateId",
                table: "Resumes",
                column: "ResumeTemplateId",
                principalTable: "ResumeTemplate",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_ResumeTemplate_ResumeTemplateId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_ResumeTemplateId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ResumeTemplateId",
                table: "Resumes");
        }
    }
}
