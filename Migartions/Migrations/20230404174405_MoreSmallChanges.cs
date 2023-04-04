using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migartions.Migrations
{
    public partial class MoreSmallChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSportsman_Sportsmens_SportsmanId",
                table: "EventSportsman");

            migrationBuilder.DropForeignKey(
                name: "FK_SportsmanCompetition_Sportsmens_SportsmanId",
                table: "SportsmanCompetition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sportsmens",
                table: "Sportsmens");

            migrationBuilder.RenameTable(
                name: "Sportsmens",
                newName: "Sportsmans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sportsmans",
                table: "Sportsmans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSportsman_Sportsmans_SportsmanId",
                table: "EventSportsman",
                column: "SportsmanId",
                principalTable: "Sportsmans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportsmanCompetition_Sportsmans_SportsmanId",
                table: "SportsmanCompetition",
                column: "SportsmanId",
                principalTable: "Sportsmans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSportsman_Sportsmans_SportsmanId",
                table: "EventSportsman");

            migrationBuilder.DropForeignKey(
                name: "FK_SportsmanCompetition_Sportsmans_SportsmanId",
                table: "SportsmanCompetition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sportsmans",
                table: "Sportsmans");

            migrationBuilder.RenameTable(
                name: "Sportsmans",
                newName: "Sportsmens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sportsmens",
                table: "Sportsmens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSportsman_Sportsmens_SportsmanId",
                table: "EventSportsman",
                column: "SportsmanId",
                principalTable: "Sportsmens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportsmanCompetition_Sportsmens_SportsmanId",
                table: "SportsmanCompetition",
                column: "SportsmanId",
                principalTable: "Sportsmens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
