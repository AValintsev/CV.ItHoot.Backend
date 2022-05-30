using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class Position : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Resumes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_PositionId",
                table: "Resumes",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Positions_PositionId",
                table: "Resumes",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Positions_PositionId",
                table: "Resumes");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_PositionId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Resumes");
        }
    }
}
