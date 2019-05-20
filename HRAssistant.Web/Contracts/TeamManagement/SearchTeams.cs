using System;
using HRAssistant.Web.Infrastructure;

namespace HRAssistant.Web.Contracts.TeamManagement
{
    public sealed class SearchTeams : SearchRequest<SearchTeamsResult>
    {
        public string Title { get; set; }

        public string TeamLeadFullName { get; set; }

        public string CityTitle { get; set; }

        public bool? IsBlocked { get; set; }
    }
}
