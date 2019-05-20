using HRAssistant.Web.Contracts.TeamManagement;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.UseCases.Mapping
{
    public static class TeamMapperExtensions
    {
        public static void Update(this TeamEntity entity, Team team)
        {
            entity.Title = team.Title;
            entity.TeamLeadId = team.TeamLeadId.Value;
            entity.CityId = team.CityId.Value;
            entity.IsBlocked = team.IsBlocked.Value;
        }
    }
}
