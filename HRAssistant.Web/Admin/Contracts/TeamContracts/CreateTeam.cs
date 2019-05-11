using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.TeamContracts
{
    public sealed class CreateTeam : ICommand<CreateTeamResult>
    {
        public Team Team { get; set; }
    }
}
