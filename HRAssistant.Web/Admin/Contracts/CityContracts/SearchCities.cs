using HRAssistant.Web.Infrastructure;

namespace HRAssistant.Web.Admin.Contracts.CityContracts
{
    public sealed class SearchCities : SearchRequest<SearchCitiesResult>
    {
        public string Name { get; set; }
    }
}
