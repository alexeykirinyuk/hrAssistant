using System.Threading.Tasks;
using HRAssistant.Web.Contracts.TeamManagement;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.TeamManagement
{
    internal sealed class GetTeamHandler : IQueryHandler<GetTeam, GetTeamResult>
    {
        private readonly ITeamRepository _teamRepository;

        public GetTeamHandler(ITeamRepository teamRepository)
        {
            Guard.AgainstNullArgument(nameof(teamRepository), teamRepository);

            _teamRepository = teamRepository;
        }

        public async Task<GetTeamResult> Handle(GetTeam query)
        {
            var team = await _teamRepository.Get(query.TeamId.Value);

            return new GetTeamResult
            {
                Team = new Team
                {
                    Id = team.Id,
                    Title = team.Title,
                    TeamLeadId = team.TeamLeadId,
                    CityId = team.CityId,
                    IsBlocked = team.IsBlocked
                }
            };
        }
    }
}
