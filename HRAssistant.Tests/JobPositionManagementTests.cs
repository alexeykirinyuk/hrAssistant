﻿using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.Contracts.Shared;
using Xunit;

namespace HRAssistant.Tests
{
    public sealed class JobPositionManagementTests : Tests
    {
        private readonly JobPosition _jobPosition;

        public JobPositionManagementTests()
        {
            _jobPosition = new JobPosition
            {
                Title = UniqueUtils.MakeUnique("Junior Java Developer"),
                Template = new Template
                {
                    Description = "Please, answer on questions",
                    Questions = new Question[]
                    {
                        new GeneralQuestion
                        {
                            Title = "How are you?",
                            Description = "Please, describe your filling.",
                            OrderIndex = 0,
                            MaxAnswerSeconds = 60
                        },
                        new InputQuestion
                        {
                            Title = "1 + 2",
                            Description = "Please answer:",
                            OrderIndex = 1,
                            CorrectAnswer = "3",
                            MaxAnswerSeconds = 60
                        },
                        new SelectQuestion
                        {
                            Title = "1 + 3",
                            Description = "Some Description",
                            OrderIndex = 2,
                            Options = new[]
                            {
                                new Option {Title = "4", IsCorrect = true},
                                new Option {Title = "5", IsCorrect = false},
                                new Option {Title = "4.0", IsCorrect = true}
                            },
                            OneCorrectAnswer = false,
                            MaxAnswerSeconds = 60
                        }
                    }
                }
            };
        }

        [Fact]
        public async Task Create()
        {
            _jobPosition.Id = (await Bus.Request(new CreateJobPosition {JobPosition = _jobPosition}))
                .JobPositionId;

            var receivedJobPosition = (await Bus.Execute(new GetJobPosition {JobPositionId = _jobPosition.Id.Value}))
                .JobPosition;

            receivedJobPosition
                .Should()
                .BeEquivalentTo(_jobPosition);

            var allPositions = (await Bus.Execute(new SearchJobPositions {Title = _jobPosition.Title}))
                .Items;

            allPositions
                .Should()
                .ContainSingle();

            var position = allPositions.Single(p => p.JobPositionId == _jobPosition.Id.Value);
            position.Title.Should().Be(_jobPosition.Title);
        }

        [Fact]
        public async Task Update()
        {
            _jobPosition.Id = (await Bus.Request(new CreateJobPosition {JobPosition = _jobPosition}))
                .JobPositionId;
            _jobPosition.Title = UniqueUtils.MakeUnique("New Title");
            _jobPosition.Template = new Template
            {
                Description = "Changed Description",
                Questions = new Question[]
                {
                    new GeneralQuestion
                    {
                        Title = "New Question",
                        Description = "New Description",
                        OrderIndex = 1,
                        MaxAnswerSeconds = 60
                    },
                    new InputQuestion
                    {
                        Title = "Changed Question",
                        Description = "Changed Description",
                        CorrectAnswer = "Changed Answer",
                        OrderIndex = 2,
                        MaxAnswerSeconds = 60
                    }
                }
            };

            await Bus.Request(new UpdateJobPosition {JobPosition = _jobPosition});

            var receivedJobPosition = (await Bus.Execute(new GetJobPosition {JobPositionId = _jobPosition.Id.Value}))
                .JobPosition;

            receivedJobPosition.Should()
                .BeEquivalentTo(_jobPosition);

            var allPositions = (await Bus.Execute(new SearchJobPositions()))
                .Items;

            allPositions
                .Should()
                .ContainSingle(p => p.JobPositionId == _jobPosition.Id.Value);

            var position = allPositions.Single(p => p.JobPositionId == _jobPosition.Id.Value);
            position.Title.Should().Be(_jobPosition.Title);
        }
    }
}