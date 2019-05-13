using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.CityManagement;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.CityManagement
{
    internal sealed class SearchCitiesHandler : IQueryHandler<SearchCities, SearchCitiesResult>
    {
        private readonly ICityRepository _cityRepository;

        public SearchCitiesHandler(ICityRepository cityRepository)
        {
            Guard.AgainstNullArgument(nameof(cityRepository), cityRepository);

            _cityRepository = cityRepository;
        }

        public Task<SearchCitiesResult> Handle(SearchCities query)
        {
            return _cityRepository.Search()
                .FilterBy(query.Name, entity => entity.Name.Contains(query.Name))
                .Select(c => new City
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToSearchResults(query);
        }
    }
}
