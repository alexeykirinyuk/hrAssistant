using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HRAssistant.Web.Admin.Contracts.JobPositionContracts;
using HRAssistant.Web.Admin.Contracts.TeamContracts;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using Xunit;

namespace HRAssistant.Tests
{
    public sealed class VacancyManagementTests : Tests, IAsyncLifetime
    {
        private Guid _jobPositionId;
        private Guid _teamId;

        [Fact]
        public async Task Lifecycle()
        {
            // Create Vacancy
            var jobPosition = (await Bus.Execute(new GetJobPosition {JobPositionId = _jobPositionId})).JobPosition;
            var vacancy = new Vacancy
            {
                JobPositionId = _jobPositionId,
                TeamId = _teamId,
                Salary = 70_000,
                CandidateRequirements = "Some requriements",
                JobsNumber = 50,
                Form = new Form
                {
                    Description = jobPosition.Template.Description,
                    Questions = jobPosition.Template.Questions
                }
            };

            vacancy.Id = (await Bus.Request(new CreateVacancy {Vacancy = vacancy}))
                .VacancyId;

            // Get Vacancy
            var updateVacancy = (await Bus.Execute(new GetVacancy {VacancyId = vacancy.Id.Value})).Vacancy;
            updateVacancy
                .Should()
                .BeEquivalentTo(vacancy, options => options.Excluding(v => v.Status));
            updateVacancy.Status.Should().Be(VacancyStatus.Draft);

            // Update Vacancy
            var newTeam = await CreateTeam();
            updateVacancy.TeamId = newTeam.Id;
            updateVacancy.JobsNumber = 500;
            updateVacancy.CandidateRequirements = "New Requirements";
            updateVacancy.Salary = 100_000;
            updateVacancy.Status = null;
            updateVacancy.Form.Description = "New Description";
            updateVacancy.Form.Questions = updateVacancy.Form.Questions.Append(new GeneralQuestion
            {
                Title = "Ещё один вопрос",
                Description = "Некоторое описание",
                OrderIndex = 10
            }).ToArray();

            await Bus.Request(new UpdateVacancy {Vacancy = updateVacancy});

            // Get Vacancy
            var updatedVacancy = (await Bus.Execute(new GetVacancy {VacancyId = updateVacancy.Id})).Vacancy;

            updatedVacancy
                .Should()
                .BeEquivalentTo(updateVacancy, options => options.Excluding(v => v.Status));
            updatedVacancy.Status.Should().Be(VacancyStatus.Draft);

            // Open Vacancy
            await Bus.Request(new OpenVacancy {VacancyId = vacancy.Id.Value});

            (await Bus.Execute(new GetVacancy {VacancyId = vacancy.Id.Value}))
                .Vacancy
                .Status
                .Should()
                .Be(VacancyStatus.Opened);

            // Close Vacancy
            await Bus.Request(new CloseVacancy {VacancyId = vacancy.Id.Value});

            (await Bus.Execute(new GetVacancy {VacancyId = vacancy.Id.Value}))
                .Vacancy
                .Status
                .Should()
                .Be(VacancyStatus.Closed);
        }

        private async Task<Team> CreateTeam()
        {
            var city = await TestApi.CreateCity();
            var teamLead = await TestApi.CreateUser();
            
            return await TestApi.CreateTeam(teamLead, city);
        }

        public async Task InitializeAsync()
        {
            _jobPositionId = (await TestApi.CreateJobPosition()).Id.Value;
           _teamId = (await CreateTeam()).Id.Value;
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
