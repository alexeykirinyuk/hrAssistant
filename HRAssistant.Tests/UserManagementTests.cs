using System.Threading.Tasks;
using FluentAssertions;
using HRAssistant.Admin.Contracts.UserContracts;
using Xunit;

namespace HRAssistant.Tests
{
    public class UserManagementTests : Tests
    {
        private User _testUser;

        public UserManagementTests()
        {
            _testUser = new User
            {
                FirstName = "Alexey",
                LastName = "Gaiist",
                IsBlocked = false,
                Password = "testpassword",
                Role = Role.HR,
                Username = UniqueHelper.MakeUnique("akirinyuk")
            };
        }

        [Fact]
        public async Task Create()
        {
            // Given
            var userId = (await Bus.Request(new AddUser {User = _testUser}))
                .UserId;

            // When
            var receivedUser = (await Bus.Execute(new GetUser {UserId = userId}))
                .User;

            // Then
            receivedUser.Should()
                .BeEquivalentTo(_testUser, options => options.Excluding(u => u.Id));

            // When 2
            var searchUsers = (await Bus.Execute(new SearchUsers {Username = _testUser.Username}))
                .Items;

            // Then 2
            searchUsers
                .Should()
                .HaveCount(1);
        }

        [Fact]
        public async Task Update()
        {
            // Given
            _testUser.Id = (await Bus.Request(new AddUser {User = _testUser}))
                .UserId;
            _testUser.FirstName = "Roman";
            _testUser.IsBlocked = true;
            _testUser.LastName = "Antonov";
            _testUser.Password = "testpwd2";
            _testUser.Role = Role.TeamLead;
            _testUser.Username = UniqueHelper.MakeUnique("rantonov");

            await Bus.Request(new UpdateUser {User = _testUser});

            // When
            var receivedUser = (await Bus.Execute(new GetUser {UserId = _testUser.Id.Value}))
                .User;

            // Then
            receivedUser.Should()
                .BeEquivalentTo(_testUser);

            // When 2
            var searchUsers = (await Bus.Execute(new SearchUsers {Username = _testUser.Username}))
                .Items;

            // Then 2
            searchUsers
                .Should()
                .HaveCount(1);
        }
    }
}