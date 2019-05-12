using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.TeamContracts
{
    public sealed class UpdateTeam : ICommand
    {
        public Team Team { get; set; }
    }
}