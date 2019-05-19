using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRAssistant.Web.Migrations
{
    public partial class AddInterviewWorkflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxAnswerSeconds",
                table: "Question",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SelectQuestionSagaEntityId",
                table: "Option",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormSaga",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InterviewId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormSaga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interview",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VacancyId = table.Column<Guid>(nullable: false),
                    CandidateId = table.Column<Guid>(nullable: false),
                    FormSagaId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interview_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interview_FormSaga_FormSagaId",
                        column: x => x.FormSagaId,
                        principalTable: "FormSaga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interview_Vacancy_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionSaga",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    FormSagaId = table.Column<Guid>(nullable: true),
                    StartUtcTime = table.Column<DateTime>(nullable: true),
                    EndUtcTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Result = table.Column<bool>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    InputQuestionSagaEntity_Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSaga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionSaga_FormSaga_FormSagaId",
                        column: x => x.FormSagaId,
                        principalTable: "FormSaga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionSaga_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Option_SelectQuestionSagaEntityId",
                table: "Option",
                column: "SelectQuestionSagaEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_CandidateId",
                table: "Interview",
                column: "CandidateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interview_FormSagaId",
                table: "Interview",
                column: "FormSagaId",
                unique: true,
                filter: "[FormSagaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_VacancyId",
                table: "Interview",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSaga_FormSagaId",
                table: "QuestionSaga",
                column: "FormSagaId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSaga_QuestionId",
                table: "QuestionSaga",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Option_QuestionSaga_SelectQuestionSagaEntityId",
                table: "Option",
                column: "SelectQuestionSagaEntityId",
                principalTable: "QuestionSaga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Option_QuestionSaga_SelectQuestionSagaEntityId",
                table: "Option");

            migrationBuilder.DropTable(
                name: "Interview");

            migrationBuilder.DropTable(
                name: "QuestionSaga");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "FormSaga");

            migrationBuilder.DropIndex(
                name: "IX_Option_SelectQuestionSagaEntityId",
                table: "Option");

            migrationBuilder.DropColumn(
                name: "MaxAnswerSeconds",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "SelectQuestionSagaEntityId",
                table: "Option");
        }
    }
}
