using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using HRAssistant.Web.Admin.UseCases.Mapping;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
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
            var statuses = query.Statuses?.Select(s => s.CreateDomain()).ToArray();
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
                    JobPositionTitle = v.JobPosition.Title,
                    TeamTitle = v.Team.Title
                    
                })
                .ToSearchResults(query);
        }
    }
}
