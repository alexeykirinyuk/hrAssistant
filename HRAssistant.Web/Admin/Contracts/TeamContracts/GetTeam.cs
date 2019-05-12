using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.TeamContracts
{
    public class GetTeam : IQuery<GetTeamResult>
    {
        public Guid? TeamId { get; set; }
    }
}
