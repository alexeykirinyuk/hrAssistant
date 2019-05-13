using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HRAssistant.Web.Contracts.CityManagement;
using Xunit;

namespace HRAssistant.Tests
{
    public sealed class CityManagementTests : Tests
    {
        private readonly City _city;

        public CityManagementTests()
        {
            _city = new City
            {
                Name = UniqueHelper.MakeUnique("Karaganda")
            };
        }

        [Fact]
        public async Task Create()
        {
            _city.Id = (await Bus.Request(new CreateCity {City = _city})).CityId;

            (await Bus.Execute(new GetCity {CityId = _city.Id}))
                .City
                .Should()
                .BeEquivalentTo(_city);

            await AssertHasInSearchResults();
        }

        [Fact]
        public async Task Update()
        {
            _city.Id = (await Bus.Request(new CreateCity {City = _city})).CityId;

            _city.Name = UniqueHelper.MakeUnique("Moskow");
            var updateCity = new UpdateCity {City = _city};
            await Bus.Request(updateCity);

            (await Bus.Execute(new GetCity {CityId = _city.Id}))
                .City
                .Should()
                .BeEquivalentTo(_city);

            await AssertHasInSearchResults();
        }

        private async Task AssertHasInSearchResults()
        {
            var items = (await Bus.Execute(new SearchCities {Name = _city.Name}))
                .Items;

            items
                .Should()
                .HaveCount(1);

            items.Single()
                .Should()
                .BeEquivalentTo(_city);
        }
    }
}
