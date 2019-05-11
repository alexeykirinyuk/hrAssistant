using HRAssistant.Infrastructure;

namespace HRAssistant.Web.Admin.Contracts.TeamContracts
{
    public sealed class SearchTeams : SearchRequest<SearchTeamsResult>
    {
        public string Title { get; set; }
    }
}
