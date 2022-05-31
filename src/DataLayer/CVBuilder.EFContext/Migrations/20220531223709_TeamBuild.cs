using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class TeamBuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamBuildComplexity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplexityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamBuildComplexity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamBuilds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplexityId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamBuilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamBuilds_TeamBuildComplexity_ComplexityId",
                        column: x => x.ComplexityId,
                        principalTable: "TeamBuildComplexity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TeamBuildPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    TeamBuildId = table.Column<int>(type: "int", nullable: false),
                    CountMembers = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamBuildPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamBuildPosition_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamBuildPosition_TeamBuilds_TeamBuildId",
                        column: x => x.TeamBuildId,
                        principalTable: "TeamBuilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamBuildPosition_PositionId",
                table: "TeamBuildPosition",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamBuildPosition_TeamBuildId",
                table: "TeamBuildPosition",
                column: "TeamBuildId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamBuilds_ComplexityId",
                table: "TeamBuilds",
                column: "ComplexityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamBuildPosition");

            migrationBuilder.DropTable(
                name: "TeamBuilds");

            migrationBuilder.DropTable(
                name: "TeamBuildComplexity");
        }
    }
}
