using Microsoft.EntityFrameworkCore.Migrations;

namespace HRAssistant.Web.Migrations
{
    public partial class Addedinterviewresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_InterviewResultEntity_ResultId",
                table: "Interview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterviewResultEntity",
                table: "InterviewResultEntity");

            migrationBuilder.RenameTable(
                name: "InterviewResultEntity",
                newName: "InterviewResult");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterviewResult",
                table: "InterviewResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_InterviewResult_ResultId",
                table: "Interview",
                column: "ResultId",
                principalTable: "InterviewResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_InterviewResult_ResultId",
                table: "Interview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterviewResult",
                table: "InterviewResult");

            migrationBuilder.RenameTable(
                name: "InterviewResult",
                newName: "InterviewResultEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterviewResultEntity",
                table: "InterviewResultEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_InterviewResultEntity_ResultId",
                table: "Interview",
                column: "ResultId",
                principalTable: "InterviewResultEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
