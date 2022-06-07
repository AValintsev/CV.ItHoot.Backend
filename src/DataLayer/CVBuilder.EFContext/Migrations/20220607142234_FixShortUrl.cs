using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class FixShortUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamResume_Resumes_ResumeId",
                table: "TeamResume");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamResume_Teams_TeamId",
                table: "TeamResume");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamResume",
                table: "TeamResume");

            migrationBuilder.DropColumn(
                name: "ShortAuthUrl",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TeamResume",
                newName: "TeamResumes");

            migrationBuilder.RenameIndex(
                name: "IX_TeamResume_TeamId",
                table: "TeamResumes",
                newName: "IX_TeamResumes_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamResume_ResumeId",
                table: "TeamResumes",
                newName: "IX_TeamResumes_ResumeId");

            migrationBuilder.AddColumn<int>(
                name: "ShortUrlId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShortUrlId",
                table: "TeamResumes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamResumes",
                table: "TeamResumes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ShortUrl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrl", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShortUrlId",
                table: "AspNetUsers",
                column: "ShortUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamResumes_ShortUrlId",
                table: "TeamResumes",
                column: "ShortUrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShortUrl_ShortUrlId",
                table: "AspNetUsers",
                column: "ShortUrlId",
                principalTable: "ShortUrl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResumes_Resumes_ResumeId",
                table: "TeamResumes",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResumes_ShortUrl_ShortUrlId",
                table: "TeamResumes",
                column: "ShortUrlId",
                principalTable: "ShortUrl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResumes_Teams_TeamId",
                table: "TeamResumes",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShortUrl_ShortUrlId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamResumes_Resumes_ResumeId",
                table: "TeamResumes");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamResumes_ShortUrl_ShortUrlId",
                table: "TeamResumes");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamResumes_Teams_TeamId",
                table: "TeamResumes");

            migrationBuilder.DropTable(
                name: "ShortUrl");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShortUrlId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamResumes",
                table: "TeamResumes");

            migrationBuilder.DropIndex(
                name: "IX_TeamResumes_ShortUrlId",
                table: "TeamResumes");

            migrationBuilder.DropColumn(
                name: "ShortUrlId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShortUrlId",
                table: "TeamResumes");

            migrationBuilder.RenameTable(
                name: "TeamResumes",
                newName: "TeamResume");

            migrationBuilder.RenameIndex(
                name: "IX_TeamResumes_TeamId",
                table: "TeamResume",
                newName: "IX_TeamResume_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamResumes_ResumeId",
                table: "TeamResume",
                newName: "IX_TeamResume_ResumeId");

            migrationBuilder.AddColumn<string>(
                name: "ShortAuthUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamResume",
                table: "TeamResume",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResume_Resumes_ResumeId",
                table: "TeamResume",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamResume_Teams_TeamId",
                table: "TeamResume",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
