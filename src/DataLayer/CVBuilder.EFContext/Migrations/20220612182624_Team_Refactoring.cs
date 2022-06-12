using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.EFContext.Migrations
{
    public partial class Team_Refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamBuildPosition");

            migrationBuilder.DropTable(
                name: "TeamResumes");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "TeamBuilds");

            migrationBuilder.DropTable(
                name: "TeamBuildComplexity");

            migrationBuilder.CreateTable(
                name: "ProposalBuildComplexity",
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
                    table.PrimaryKey("PK_ProposalBuildComplexity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProposalBuilds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estimation = table.Column<int>(type: "int", nullable: false),
                    ComplexityId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalBuilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalBuilds_ProposalBuildComplexity_ComplexityId",
                        column: x => x.ComplexityId,
                        principalTable: "ProposalBuildComplexity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Proposal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: true),
                    ShowLogo = table.Column<bool>(type: "bit", nullable: false),
                    ShowContacts = table.Column<bool>(type: "bit", nullable: false),
                    ShowCompanyNames = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ProposalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResumeTemplateId = table.Column<int>(type: "int", nullable: false),
                    ProposalBuildId = table.Column<int>(type: "int", nullable: true),
                    StatusProposal = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposal_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposal_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposal_ProposalBuilds_ProposalBuildId",
                        column: x => x.ProposalBuildId,
                        principalTable: "ProposalBuilds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposal_ResumeTemplate_ResumeTemplateId",
                        column: x => x.ResumeTemplateId,
                        principalTable: "ResumeTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposalBuildPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    ProposalBuildId = table.Column<int>(type: "int", nullable: false),
                    CountMembers = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalBuildPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalBuildPosition_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProposalBuildPosition_ProposalBuilds_ProposalBuildId",
                        column: x => x.ProposalBuildId,
                        principalTable: "ProposalBuilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposalResumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    StatusResume = table.Column<int>(type: "int", nullable: false),
                    ShortUrlId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalResumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalResumes_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProposalResumes_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProposalResumes_ShortUrl_ShortUrlId",
                        column: x => x.ShortUrlId,
                        principalTable: "ShortUrl",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_ClientId",
                table: "Proposal",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_CreatedUserId",
                table: "Proposal",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_ProposalBuildId",
                table: "Proposal",
                column: "ProposalBuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_ResumeTemplateId",
                table: "Proposal",
                column: "ResumeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalBuildPosition_PositionId",
                table: "ProposalBuildPosition",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalBuildPosition_ProposalBuildId",
                table: "ProposalBuildPosition",
                column: "ProposalBuildId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalBuilds_ComplexityId",
                table: "ProposalBuilds",
                column: "ComplexityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalResumes_ProposalId",
                table: "ProposalResumes",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalResumes_ResumeId",
                table: "ProposalResumes",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalResumes_ShortUrlId",
                table: "ProposalResumes",
                column: "ShortUrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProposalBuildPosition");

            migrationBuilder.DropTable(
                name: "ProposalResumes");

            migrationBuilder.DropTable(
                name: "Proposal");

            migrationBuilder.DropTable(
                name: "ProposalBuilds");

            migrationBuilder.DropTable(
                name: "ProposalBuildComplexity");

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
                    ComplexityId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estimation = table.Column<int>(type: "int", nullable: false),
                    ProjectTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserId = table.Column<int>(type: "int", nullable: true),
                    ResumeTemplateId = table.Column<int>(type: "int", nullable: false),
                    TeamBuildId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShowCompanyNames = table.Column<bool>(type: "bit", nullable: false),
                    ShowContacts = table.Column<bool>(type: "bit", nullable: false),
                    ShowLogo = table.Column<bool>(type: "bit", nullable: false),
                    StatusTeam = table.Column<int>(type: "int", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_ResumeTemplate_ResumeTemplateId",
                        column: x => x.ResumeTemplateId,
                        principalTable: "ResumeTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_TeamBuilds_TeamBuildId",
                        column: x => x.TeamBuildId,
                        principalTable: "TeamBuilds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeamResumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    ShortUrlId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusResume = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamResumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamResumes_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamResumes_ShortUrl_ShortUrlId",
                        column: x => x.ShortUrlId,
                        principalTable: "ShortUrl",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamResumes_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
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

            migrationBuilder.CreateIndex(
                name: "IX_TeamResumes_ResumeId",
                table: "TeamResumes",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamResumes_ShortUrlId",
                table: "TeamResumes",
                column: "ShortUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamResumes_TeamId",
                table: "TeamResumes",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ClientId",
                table: "Teams",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CreatedUserId",
                table: "Teams",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ResumeTemplateId",
                table: "Teams",
                column: "ResumeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamBuildId",
                table: "Teams",
                column: "TeamBuildId");
        }
    }
}
