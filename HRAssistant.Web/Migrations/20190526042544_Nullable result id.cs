using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRAssistant.Web.Migrations
{
    public partial class Nullableresultid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_InterviewResult_ResultId",
                table: "Interview");

            migrationBuilder.DropIndex(
                name: "IX_Interview_ResultId",
                table: "Interview");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResultId",
                table: "Interview",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Interview_ResultId",
                table: "Interview",
                column: "ResultId",
                unique: true,
                filter: "[ResultId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_InterviewResult_ResultId",
                table: "Interview",
                column: "ResultId",
                principalTable: "InterviewResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_InterviewResult_ResultId",
                table: "Interview");

            migrationBuilder.DropIndex(
                name: "IX_Interview_ResultId",
                table: "Interview");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResultId",
                table: "Interview",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interview_ResultId",
                table: "Interview",
                column: "ResultId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_InterviewResult_ResultId",
                table: "Interview",
                column: "ResultId",
                principalTable: "InterviewResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
