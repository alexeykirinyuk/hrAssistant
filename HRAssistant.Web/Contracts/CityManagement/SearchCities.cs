using HRAssistant.Web.Infrastructure;

namespace HRAssistant.Web.Contracts.CityManagement
{
    public sealed class SearchCities : SearchRequest<SearchCitiesResult>
    {
        public string Name { get; set; }
    }
}
