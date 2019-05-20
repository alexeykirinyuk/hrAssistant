using System.Threading.Tasks;
using HRAssistant.Web.Contracts.CityManagement;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.Contracts.Shared;
using HRAssistant.Web.Contracts.TeamManagement;
using HRAssistant.Web.Contracts.UserManagement;
using HRAssistant.Web.Contracts.VacancyManagement;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Tests
{
    public sealed class TestApi
    {
        private readonly IBus _bus;

        public TestApi(IBus bus)
        {
            Guard.AgainstNullArgument(nameof(bus), bus);

            _bus = bus;
        }

        public async Task<City> CreateCity()
        {
            var city = new City
            {
                Name = UniqueUtils.MakeUnique("Novosibirsk")
            };

            city.Id = (await _bus.Request(new CreateCity {City = city}))
                .CityId;

            return city;
        }

        public async Task<User> CreateUser()
        {
            var user = new User
            {
                FirstName = "Alexey",
                LastName = "Gaiist",
                IsBlocked = false,
                Password = "testpassword",
                Role = Role.HR,
                Username = UniqueUtils.MakeUnique("akirinyuk")
            };

            user.Id = (await _bus.Request(new AddUser {User = user}))
                .UserId;

            return user;
        }

        public async Task<Team> CreateTeam(User teamLead, City city)
        {
            var team = new Team
            {
                Title = UniqueUtils.MakeUnique("Alexey Kirinyuk team"),
                CityId = city.Id,
                TeamLeadId = teamLead.Id,
                IsBlocked = false
            };

            team.Id = (await _bus.Request(new CreateTeam {Team = team}))
                .TeamId;

            return team;
        }

        public async Task<JobPosition> CreateJobPosition()
        {
            var jobPosition = new JobPosition
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
                            MaxAnswerSeconds = 50
                        },
                        new InputQuestion
                        {
                            Title = "1 + 2",
                            Description = "Please answer:",
                            OrderIndex = 1,
                            CorrectAnswer = "3",
                            MaxAnswerSeconds = 50
                        },
                        new SelectQuestion
                        {
                            Title = "1 + 3",
                            Description = "Please selectамана",
                            OrderIndex = 2,
                            Options = new[]
                            {
                                new Option {Title = "4", IsCorrect = true},
                                new Option {Title = "5", IsCorrect = false},
                                new Option {Title = "4.0", IsCorrect = true}
                            },
                            OneCorrectAnswer = false,
                            MaxAnswerSeconds = 50
                        }
                    }
                }
            };

            jobPosition.Id = (await _bus.Request(new CreateJobPosition {JobPosition = jobPosition}))
                .JobPositionId;

            return jobPosition;
        }

        public async Task<Vacancy> CreateVacancy(JobPosition jobPosition, Team team)
        {
            var vacancy = new Vacancy
            {
                JobPositionId = jobPosition.Id,
                TeamId = team.Id,
                Salary = 70_000,
                CandidateRequirements = "Some requriements",
                JobsNumber = 50,
                Form = new Form
                {
                    Description = jobPosition.Template.Description,
                    Questions = jobPosition.Template.Questions
                }
            };

            vacancy.Id = (await _bus.Request(new CreateVacancy {Vacancy = vacancy}))
                .VacancyId;
            await _bus.Request(new OpenVacancy {VacancyId = vacancy.Id});

            return vacancy;
        }
    }
}