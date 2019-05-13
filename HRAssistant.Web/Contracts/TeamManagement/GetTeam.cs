using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.TeamManagement
{
    public class GetTeam : IQuery<GetTeamResult>
    {
        public Guid? TeamId { get; set; }
    }
}
