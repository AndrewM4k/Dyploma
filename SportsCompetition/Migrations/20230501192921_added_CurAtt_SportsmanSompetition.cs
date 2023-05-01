using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsCompetition.Migrations
{
    public partial class added_CurAtt_SportsmanSompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentAttempt",
                table: "SportsmanCompetition",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAttempt",
                table: "SportsmanCompetition");
        }
    }
}
