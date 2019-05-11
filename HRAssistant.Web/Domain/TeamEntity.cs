using System;
using HRAssistant.Domain;

namespace HRAssistant.Web.Domain
{
    public sealed class TeamEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid TeamLeadId { get; set; }

        public UserEntity TeamLead { get; set; }

        public Guid CityId { get; set; }

        public CityEntity City { get; set; }

        public bool IsBlocked { get; set; }
    }
}