using System;
using System.Threading.Tasks;
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
        public async Task Create()
        {
            var jobPosition = (await Bus.Execute(new GetJobPosition {JobPositionId = _jobPositionId})).JobPosition;
            var newVacancy = new Vacancy
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

            await Bus.Request(new CreateVacancy {Vacancy = newVacancy});

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
