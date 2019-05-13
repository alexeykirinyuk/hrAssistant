using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.TeamManagement
{
    public sealed class CreateTeam : ICommand<CreateTeamResult>
    {
        public Team Team { get; set; }
    }
}
