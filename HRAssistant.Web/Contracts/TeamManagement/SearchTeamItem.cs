using System;

namespace HRAssistant.Web.Contracts.TeamManagement
{
    public sealed class SearchTeamItem
    {
        public Guid TeamId { get; set; }

        public string Title { get; set; }

        public string TeamLeadFullName { get; set; }

        public string CityTitle { get; set; }

        public bool IsBlocked { get; set; }
    }
}