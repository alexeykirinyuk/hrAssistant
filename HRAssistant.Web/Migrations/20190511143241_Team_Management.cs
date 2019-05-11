using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRAssistant.Migrations
{
    public partial class Team_Management : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    TeamLeadId = table.Column<Guid>(nullable: false),
                    CityId = table.Column<Guid>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_User_TeamLeadId",
                        column: x => x.TeamLeadId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_CityId",
                table: "Team",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamLeadId",
                table: "Team",
                column: "TeamLeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
