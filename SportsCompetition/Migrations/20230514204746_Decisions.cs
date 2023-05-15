using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsCompetition.Migrations
{
    public partial class Decisions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decision_Attempt_AttemptId",
                table: "Decision");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Decision",
                table: "Decision");

            migrationBuilder.RenameTable(
                name: "Decision",
                newName: "Decisions");

            migrationBuilder.RenameIndex(
                name: "IX_Decision_AttemptId",
                table: "Decisions",
                newName: "IX_Decisions_AttemptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Decisions",
                table: "Decisions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decisions_Attempt_AttemptId",
                table: "Decisions",
                column: "AttemptId",
                principalTable: "Attempt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decisions_Attempt_AttemptId",
                table: "Decisions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Decisions",
                table: "Decisions");

            migrationBuilder.RenameTable(
                name: "Decisions",
                newName: "Decision");

            migrationBuilder.RenameIndex(
                name: "IX_Decisions_AttemptId",
                table: "Decision",
                newName: "IX_Decision_AttemptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Decision",
                table: "Decision",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decision_Attempt_AttemptId",
                table: "Decision",
                column: "AttemptId",
                principalTable: "Attempt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
