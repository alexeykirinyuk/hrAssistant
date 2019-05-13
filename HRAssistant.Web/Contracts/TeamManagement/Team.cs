using System;

namespace HRAssistant.Web.Contracts.TeamManagement
{
    public sealed class Team
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public Guid? TeamLeadId { get; set; }

        public Guid? CityId { get; set; }

        public bool? IsBlocked { get; set; }
    }
}
