using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migartions.Migrations
{
    public partial class DB_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Record_Competition_CompetitionId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_Standart_Competition_CompetitionId",
                table: "Standart");

            migrationBuilder.DropTable(
                name: "MovementCompetition");

            migrationBuilder.DropTable(
                name: "Movement");

            migrationBuilder.DropIndex(
                name: "IX_Standart_CompetitionId",
                table: "Standart");

            migrationBuilder.DropIndex(
                name: "IX_Record_CompetitionId",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Standart");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "WeightStandart",
                table: "Standart",
                newName: "StandartResult");

            migrationBuilder.RenameColumn(
                name: "WeightStandart",
                table: "Record",
                newName: "RecordResult");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Sportsmans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Sportsmans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Sportsmans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<int>(
                name: "StreamNumber",
                table: "EventSportsman",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Attempt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weihgt = table.Column<int>(type: "int", nullable: false),
                    SportsmanCompetitionCompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SportsmanCompetitionSportsmanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attempt_SportsmanCompetition_SportsmanCompetitionCompetitionId_SportsmanCompetitionSportsmanId",
                        columns: x => new { x.SportsmanCompetitionCompetitionId, x.SportsmanCompetitionSportsmanId },
                        principalTable: "SportsmanCompetition",
                        principalColumns: new[] { "CompetitionId", "SportsmanId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionRecord",
                columns: table => new
                {
                    RecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionRecord", x => new { x.CompetitionId, x.RecordId });
                    table.ForeignKey(
                        name: "FK_CompetitionRecord_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionRecord_Record_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Record",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionStandart",
                columns: table => new
                {
                    StandartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionStandart", x => new { x.CompetitionId, x.StandartId });
                    table.ForeignKey(
                        name: "FK_CompetitionStandart_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionStandart_Standart_StandartId",
                        column: x => x.StandartId,
                        principalTable: "Standart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Attempt_SportsmanCompetitionCompetitionId_SportsmanCompetitionSportsmanId",
                table: "Attempt",
                columns: new[] { "SportsmanCompetitionCompetitionId", "SportsmanCompetitionSportsmanId" });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionRecord_RecordId",
                table: "CompetitionRecord",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionStandart_StandartId",
                table: "CompetitionStandart",
                column: "StandartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Role_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Role_RoleId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Attempt");

            migrationBuilder.DropTable(
                name: "CompetitionRecord");

            migrationBuilder.DropTable(
                name: "CompetitionStandart");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Sportsmans");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Sportsmans");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Sportsmans");

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
                name: "StreamNumber",
                table: "EventSportsman");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "StandartResult",
                table: "Standart",
                newName: "WeightStandart");

            migrationBuilder.RenameColumn(
                name: "RecordResult",
                table: "Record",
                newName: "WeightStandart");

            migrationBuilder.AddColumn<Guid>(
                name: "CompetitionId",
                table: "Standart",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompetitionId",
                table: "Record",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovementCompetition",
                columns: table => new
                {
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Standart_CompetitionId",
                table: "Standart",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_CompetitionId",
                table: "Record",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementCompetition_MovementId",
                table: "MovementCompetition",
                column: "MovementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Competition_CompetitionId",
                table: "Record",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Standart_Competition_CompetitionId",
                table: "Standart",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
