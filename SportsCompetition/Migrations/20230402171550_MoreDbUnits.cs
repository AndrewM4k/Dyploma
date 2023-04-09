using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsCompetition.Migrations
{
    public partial class MoreDbUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Event_EventId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Movement_Competition_CompetitionId",
                table: "Movement");

            migrationBuilder.DropIndex(
                name: "IX_Movement_CompetitionId",
                table: "Movement");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EventId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Movement");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "EmployeeEvent",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEvent", x => new { x.EmployeeId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EmployeeEvent_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeEvent_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovementCompetition",
                columns: table => new
                {
                    MovementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementCompetition", x => new { x.CompetitionId, x.MovementId });
                    table.ForeignKey(
                        name: "FK_MovementCompetition_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovementCompetition_Movement_MovementId",
                        column: x => x.MovementId,
                        principalTable: "Movement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightOfSportsman = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfRecord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightStandart = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Record_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Standart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightOfSportsman = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightStandart = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Standart_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvent_EventId",
                table: "EmployeeEvent",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementCompetition_MovementId",
                table: "MovementCompetition",
                column: "MovementId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_CompetitionId",
                table: "Record",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Standart_CompetitionId",
                table: "Standart",
                column: "CompetitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEvent");

            migrationBuilder.DropTable(
                name: "MovementCompetition");

            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "Standart");

            migrationBuilder.AddColumn<Guid>(
                name: "CompetitionId",
                table: "Movement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movement_CompetitionId",
                table: "Movement",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EventId",
                table: "Employees",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Event_EventId",
                table: "Employees",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_Competition_CompetitionId",
                table: "Movement",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id");
        }
    }
}
