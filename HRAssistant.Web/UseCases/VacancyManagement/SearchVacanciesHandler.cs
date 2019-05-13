using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.VacancyManagement;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.VacancyManagement
{
    internal sealed class SearchVacanciesHandler : IQueryHandler<SearchVacancies, SearchVacanciesResult>
    {
        private readonly IVacancyRepository _vacancyRepository;

        public SearchVacanciesHandler(IVacancyRepository vacancyRepository)
        {
            Guard.AgainstNullArgument(nameof(vacancyRepository), vacancyRepository);

            _vacancyRepository = vacancyRepository;
        }

        public Task<SearchVacanciesResult> Handle(SearchVacancies query)
        {
            var statuses = query.Statuses?.Select(s => s.CreateDomain()).ToArray() ??
                           new[] {VacancyStatusEntity.Draft, VacancyStatusEntity.Opened};
            return _vacancyRepository.Search()
                .FilterBy(query.TeamId, v => v.TeamId == query.TeamId)
                .FilterBy(query.JobPositionId, v => v.JobPositionId == query.JobPositionId)
                .FilterBy(query.Statuses, v => statuses.Contains(v.Status))
                .Select(v => new SearchVacancyItem
                {
                    VacancyId = v.Id,
                    Status = v.Status.CreateContract(),
                    JobsNumber = v.JobsNumber,
                    Salary = v.Salary,
                    JobPositionId = v.JobPositionId,
                    JobPositionTitle = v.JobPosition.Title,
                    TeamId = v.TeamId,
                    TeamTitle = v.Team.Title
                })
                .ToSearchResults(query);
        }
    }
}