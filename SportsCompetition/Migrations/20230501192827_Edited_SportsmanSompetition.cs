using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsCompetition.Migrations
{
    public partial class Edited_SportsmanSompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAttempt",
                table: "SportsmanCompetition");

            migrationBuilder.AlterColumn<bool>(
                name: "AttemptResult",
                table: "Attempt",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentAttempt",
                table: "SportsmanCompetition",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "AttemptResult",
                table: "Attempt",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
