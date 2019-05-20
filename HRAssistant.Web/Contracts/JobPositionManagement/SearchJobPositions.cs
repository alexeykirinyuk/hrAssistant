using HRAssistant.Web.Infrastructure;

namespace HRAssistant.Web.Contracts.JobPositionManagement
{
    public sealed class SearchJobPositions : SearchRequest<SearchJobPositionsResult>
    {
        public string Title { get; set; }
    }
}
