using HRAssistant.Web.Infrastructure;

namespace HRAssistant.Web.Contracts.TeamManagement
{
    public sealed class SearchTeams : SearchRequest<SearchTeamsResult>
    {
        public string Title { get; set; }
    }
}
