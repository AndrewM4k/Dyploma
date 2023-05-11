using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsCompetition.Migrations
{
    public partial class addedDecisions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Decision",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JudgeDecision = table.Column<bool>(type: "bit", nullable: false),
                    AttemptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decision_Attempt_AttemptId",
                        column: x => x.AttemptId,
                        principalTable: "Attempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Decision_AttemptId",
                table: "Decision",
                column: "AttemptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Decision");
        }
    }
}
