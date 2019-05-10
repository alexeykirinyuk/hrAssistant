using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Infrastructure;
using HRAssistant.Infrastructure.CQRS;
using HRAssistant.Web.Admin.Contracts.CityContracts;
using HRAssistant.Web.DataAccess.Core;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
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
