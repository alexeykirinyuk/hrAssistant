using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HRAssistant.Web.Admin.Contracts.CityContracts;
using HRAssistant.Web.Admin.Contracts.TeamContracts;
using HRAssistant.Web.Admin.Contracts.UserContracts;
using Xunit;

namespace HRAssistant.Tests
{
    public sealed class TeamManagementTests : Tests, IAsyncLifetime
    {
        private User _user;
        private City _city;
        private Team _team;

        public async Task InitializeAsync()
        {
            _user = new User
            {
                FirstName = "Alexey",
                LastName = "Gaiist",
                IsBlocked = false,
                Password = "testpassword",
                Role = Role.HR,
                Username = UniqueHelper.MakeUnique("akirinyuk")
            };

            _city = new City
            {
                Name = UniqueHelper.MakeUnique("Karaganda")
            };

            _city.Id = (await Bus.Request(new CreateCity {City = _city})).CityId;
            _user.Id = (await Bus.Request(new AddUser {User = _user})).UserId;

            _team = new Team
            {
                Title = UniqueHelper.MakeUnique("Alexey Kirinyuk team"),
                CityId = _city.Id,
                TeamLeadId = _user.Id,
                IsBlocked = false
            };
        }

        [Fact]
        public async Task Create()
        {
            _team.Id = (await Bus.Request(new CreateTeam {Team = _team}))
                .TeamId;

            (await Bus.Execute(new GetTeam {TeamId = _team.Id.Value}))
                .Team
                .Should()
                .BeEquivalentTo(_team);

            await AssertSearchResults();
        }

        [Fact]
        public async Task Update()
        {
            _team.Id = (await Bus.Request(new CreateTeam {Team = _team}))
                .TeamId;

            _user = new User
            {
                FirstName = "Roman",
                LastName = "Gusev",
                IsBlocked = false,
                Password = "testpassword",
                Role = Role.TeamLead,
                Username = UniqueHelper.MakeUnique("rgusev")
            };
            _user.Id = (await Bus.Request(new AddUser {User = _user})).UserId;

            _city = new City
            {
                Name = UniqueHelper.MakeUnique("Novosibirsk")
            };
            _city.Id = (await Bus.Request(new CreateCity { City = _city })).CityId;

            _team.Title = UniqueHelper.MakeUnique("New Team Title");
            _team.CityId = _city.Id;
            _team.TeamLeadId = _user.Id;
            _team.IsBlocked = true;

            await Bus.Request(new UpdateTeam {Team = _team});

            (await Bus.Execute(new GetTeam {TeamId = _team.Id.Value}))
                .Team
                .Should()
                .BeEquivalentTo(_team);

            await AssertSearchResults();
        }

        private async Task AssertSearchResults()
        {
            var items = (await Bus.Execute(new SearchTeams {Title = _team.Title})).Items;
            items.Should()
                .HaveCount(1);
            var item = items.Single();
            item.TeamId.Should().Be(_team.Id.Value);
            item.Title.Should().Be(_team.Title);
            item.CityTitle.Should().Be(_city.Name);
            item.TeamLeadFullName.Should().Be(_user.FirstName + " " + _user.LastName);
            item.IsBlocked.Should().Be(_team.IsBlocked.Value);
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}