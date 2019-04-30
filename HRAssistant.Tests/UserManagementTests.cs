using System.Threading.Tasks;
using FluentAssertions;
using HRAssistant.Admin.Contracts.UserContracts;
using Xunit;

namespace HRAssistant.Tests
{
    public class UserManagementTests : Tests
    {
        [Fact]
        public async Task Create()
        {
            var user = new User
            {
                FirstName = "Alexey",
                LastName = "Gaiist",
                IsBlocked = false,
                Password = "testpassword",
                Role = Role.HR,
                Username = UniqueHelper.MakeUnique("akirinyuk")
            };

            var userId = (await Bus.Request(new AddUser {User = user})).UserId;

            var receivedUser = await Bus.Execute(new GetUser {UserId = userId});

            receivedUser.Should()
                .BeEquivalentTo(user, options => options.Excluding(u => u.Id));
        }
    }
}