using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Infrastructure;
using HRAssistant.Infrastructure.CQRS;
using HRAssistant.Web.Admin.Contracts.TeamContracts;
using HRAssistant.Web.DataAccess.Core;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class SearchTeamsHandler : IQueryHandler<SearchTeams, SearchTeamsResult>
    {
        private readonly ITeamRepository _teamRepository;

        public SearchTeamsHandler(ITeamRepository teamRepository)
        {
            Guard.AgainstNullArgument(nameof(teamRepository), teamRepository);

            _teamRepository = teamRepository;
        }

        public Task<SearchTeamsResult> Handle(SearchTeams query) =>
            _teamRepository.Search()
                .FilterBy(query.Title, entity => entity.Title.Contains(query.Title))
                .Select(t => new SearchTeamItem
                {
                    TeamId = t.Id,
                    Title = t.Title,
                    CityTitle = t.City.Name,
                    TeamLeadFullName = t.TeamLead.FirstName + " " + t.TeamLead.LastName,
                    IsBlocked = t.IsBlocked
                })
                .ToSearchResults(query);
    }
}
