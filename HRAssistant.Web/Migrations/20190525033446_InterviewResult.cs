using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRAssistant.Web.Migrations
{
    public partial class InterviewResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResultId",
                table: "Interview",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "InterviewResultEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CorrectAnswersCount = table.Column<int>(nullable: false),
                    IncorrectAnswersCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewResultEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interview_ResultId",
                table: "Interview",
                column: "ResultId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_InterviewResultEntity_ResultId",
                table: "Interview",
                column: "ResultId",
                principalTable: "InterviewResultEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_InterviewResultEntity_ResultId",
                table: "Interview");

            migrationBuilder.DropTable(
                name: "InterviewResultEntity");

            migrationBuilder.DropIndex(
                name: "IX_Interview_ResultId",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Interview");
        }
    }
}
