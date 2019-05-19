using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.Contracts.JobPositionManagement;
using Xunit;
using GeneralQuestion = HRAssistant.Web.Contracts.JobPositionManagement.GeneralQuestion;
using InputQuestion = HRAssistant.Web.Contracts.Shared.InputQuestion;
using Option = HRAssistant.Web.Contracts.Shared.Option;
using Question = HRAssistant.Web.Contracts.Shared.Question;
using SelectQuestion = HRAssistant.Web.Contracts.Shared.SelectQuestion;

namespace HRAssistant.Tests
{
    public sealed class InterviewWorkflowTests : Tests, IAsyncLifetime
    {
        private GeneralQuestion _firstQuestion;
        private InputQuestion _secondQuestion;
        private SelectQuestion _thirdQuestion;
        private Guid _vacancyId;
        private Guid _interviewId;

        public async Task InitializeAsync()
        {
            var teamLead = await TestApi.CreateUser();
            var city = await TestApi.CreateCity();
            var team = await TestApi.CreateTeam(teamLead, city);

            _firstQuestion = new GeneralQuestion
            {
                Title = "How are you?",
                Description = "Please, describe your filling.",
                OrderIndex = 0,
                MaxAnswerSeconds = 60
            };
            _secondQuestion = new InputQuestion
            {
                Title = "1 + 2",
                Description = "Please answer:",
                OrderIndex = 1,
                CorrectAnswer = "3",
                MaxAnswerSeconds = 60
            };
            _thirdQuestion = new SelectQuestion
            {
                Title = "1 + 3",
                Description = "",
                OrderIndex = 2,
                Options = new[]
                {
                    new Option {Title = "4", IsCorrect = false},
                    new Option {Title = "5", IsCorrect = false},
                    new Option {Title = "4.0", IsCorrect = true}
                },
                OneCorrectAnswer = true,
                MaxAnswerSeconds = 60
            };
            var jobPosition = new JobPosition
            {
                Title = "Junior Java Developer",
                Template = new Template
                {
                    Description = "Please, answer on questions",
                    Questions = new Question[]
                    {
                        _firstQuestion,
                        _secondQuestion,
                        _thirdQuestion
                    }
                }
            };

            jobPosition.Id = (await Bus.Request(new CreateJobPosition {JobPosition = jobPosition}))
                .JobPositionId;

            _vacancyId = (await TestApi.CreateVacancy(jobPosition, team)).Id.Value;
        }

        [Fact]
        public async Task SysTest()
        {
            _interviewId = (await Bus.Request(new SetContactInformation
                {
                    VacancyId = _vacancyId,
                    Email = "kkoreneva@outlook.com",
                    Phone = "89995488889",
                    FirstName = "Kristina",
                    LastName = "Koreneva"
                }))
                .InterviewId;

            _interviewId.Should().NotBeEmpty();

            await Bus.Request(new StartInterview {InterviewId = _interviewId});

            await AnswerFirstQuestion();
            await AnswerSecondQuestion();
            await AnswerThirdQuestion();
        }

        private async Task AnswerFirstQuestion()
        {
            var question = (await Bus.Request(new StartQuestion {InterviewId = _interviewId}))
                .Question;

            question.Should().NotBeNull();
            question.Should().BeAssignableTo<Web.Contracts.InterviewWorkflow.GeneralQuestion>();

            var generalQuestion = (Web.Contracts.InterviewWorkflow.GeneralQuestion) question;
            generalQuestion
                .Should()
                .BeEquivalentTo(_firstQuestion, options => options.Excluding(g => g.MaxAnswerSeconds));

            var result = await Bus.Request(new Answer
            {
                InterviewId = _interviewId,
                Value = "My name is Kristina. I'm feeling me better and better every day! :)"
            });

            result.Should().NotBeNull();
            result.Result.Should().BeNull();
        }

        private async Task AnswerSecondQuestion()
        {
            var question = (await Bus.Request(new StartQuestion {InterviewId = _interviewId})).Question;

            question.Should().NotBeNull();
            question.Should().BeAssignableTo<InputQuestion>();

            var inputQuestion = (Web.Contracts.InterviewWorkflow.InputQuestion) question;
            inputQuestion
                .Should()
                .BeEquivalentTo(_secondQuestion, options => options
                    .Excluding(g => g.MaxAnswerSeconds)
                    .Excluding(g => g.CorrectAnswer));

            var result = await Bus.Request(new Answer
            {
                InterviewId = _interviewId,
                Value = "3"
            });

            result.Should().NotBeNull();
            result.Result.Should().BeTrue();
        }

        private async Task AnswerThirdQuestion()
        {
            var question = (await Bus.Request(new StartQuestion {InterviewId = _interviewId})).Question;

            question.Should().NotBeNull();
            question.Should().BeAssignableTo<SelectQuestion>();

            var selectQuestion = (Web.Contracts.InterviewWorkflow.SelectQuestion) question;
            selectQuestion
                .Should()
                .BeEquivalentTo(_thirdQuestion, options => options
                    .Excluding(g => g.MaxAnswerSeconds));

            var result = await Bus.Request(new Answer
            {
                InterviewId = _interviewId,
                Values = selectQuestion.Options.Where((_, i) => i == 0).Select(o => o.Id).ToArray()
            });

            result.Should().NotBeNull();
            result.Result.Should().BeFalse();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}