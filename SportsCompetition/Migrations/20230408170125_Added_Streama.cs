using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsCompetition.Migrations
{
    public partial class Added_Streama : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttemptNumber",
                table: "SportsmanCompetition");

            migrationBuilder.DropColumn(
                name: "AttemptState",
                table: "SportsmanCompetition");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "SportsmanCompetition");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Attempt");

            migrationBuilder.AddColumn<Guid>(
                name: "StreamId",
                table: "SportsmanCompetition",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Attempt",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Attempt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Stream",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stream", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stream_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportsmanCompetition_StreamId",
                table: "SportsmanCompetition",
                column: "StreamId");

            migrationBuilder.CreateIndex(
                name: "IX_Stream_EventId",
                table: "Stream",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsmanCompetition_Stream_StreamId",
                table: "SportsmanCompetition",
                column: "StreamId",
                principalTable: "Stream",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsmanCompetition_Stream_StreamId",
                table: "SportsmanCompetition");

            migrationBuilder.DropTable(
                name: "Stream");

            migrationBuilder.DropIndex(
                name: "IX_SportsmanCompetition_StreamId",
                table: "SportsmanCompetition");

            migrationBuilder.DropColumn(
                name: "StreamId",
                table: "SportsmanCompetition");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Attempt");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Attempt");

            migrationBuilder.AddColumn<int>(
                name: "AttemptNumber",
                table: "SportsmanCompetition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AttemptState",
                table: "SportsmanCompetition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "SportsmanCompetition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Attempt",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
