using System.Threading.Tasks;
using HRAssistant.Infrastructure.CQRS;
using HRAssistant.Web.Admin.Contracts.TeamContracts;
using HRAssistant.Web.DataAccess.Core;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
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
