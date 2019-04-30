using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HRAssistant.Admin.Contracts.UserContracts;
using Xunit;

namespace HRAssistant.Tests
{
    public class UserManagementTests : Tests
    {
        private readonly User _testUser;

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
            _testUser.Id = (await Bus.Request(new AddUser {User = _testUser}))
                .UserId;

            // When
            var receivedUser = (await Bus.Execute(new GetUser {UserId = _testUser.Id}))
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

            AssertSearchUserItemEqualUser(searchUsers);
        }

        [Fact]
        public async Task Update()
        {
            // Given There is User was created and updated
            _testUser.Id = (await Bus.Request(new AddUser {User = _testUser}))
                .UserId;
            _testUser.FirstName = "Roman";
            _testUser.IsBlocked = true;
            _testUser.LastName = "Antonov";
            _testUser.Password = "testpwd2";
            _testUser.Role = Role.TeamLead;
            _testUser.Username = UniqueHelper.MakeUnique("rantonov");

            await Bus.Request(new UpdateUser {User = _testUser});

            // When I receive user info
            var receivedUser = (await Bus.Execute(new GetUser {UserId = _testUser.Id.Value}))
                .User;

            // Then User data should be updated
            receivedUser.Should()
                .BeEquivalentTo(_testUser);

            // When I search Users by new Username
            var searchUsers = (await Bus.Execute(new SearchUsers {Username = _testUser.Username}))
                .Items;

            // Then only one User was received
            searchUsers
                .Should()
                .HaveCount(1);

            AssertSearchUserItemEqualUser(searchUsers);
        }

        private void AssertSearchUserItemEqualUser(SearchUserItem[] searchUsers)
        {
            var user = searchUsers.Single();
            user.Username.Should().Be(_testUser.Username);
            user.Blocked.Should().Be(_testUser.IsBlocked.Value);
            user.DisplayName.Should().Be(_testUser.FirstName + " " + _testUser.LastName);
            user.Role.Should().Be(_testUser.Role);
            user.UserId.Should().Be(_testUser.Id.Value);
        }
    }
}