using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class AdRdResumeTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResumeTemplateId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResumeTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeTemplate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ResumeTemplateId",
                table: "Teams",
                column: "ResumeTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_ResumeTemplate_ResumeTemplateId",
                table: "Teams",
                column: "ResumeTemplateId",
                principalTable: "ResumeTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_ResumeTemplate_ResumeTemplateId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "ResumeTemplate");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ResumeTemplateId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ResumeTemplateId",
                table: "Teams");
        }
    }
}
