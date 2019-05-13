using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.TeamManagement
{
    public sealed class UpdateTeam : ICommand
    {
        public Team Team { get; set; }
    }
}