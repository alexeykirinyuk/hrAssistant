using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    internal sealed class SearchJobPositionsHandler : IQueryHandler<SearchJobPositions, SearchJobPositionsResult>
    {
        private readonly IJobPositionRepository _jobPositionRepository;

        public SearchJobPositionsHandler(IJobPositionRepository jobPositionRepository)
        {
            Guard.AgainstNullArgument(nameof(jobPositionRepository), jobPositionRepository);

            _jobPositionRepository = jobPositionRepository;
        }

        public async Task<SearchJobPositionsResult> Handle(SearchJobPositions query)
        {
            return await _jobPositionRepository.Search()
                .Select(j => new SearchJobPositionItem
                {
                    JobPositionId = j.Id,
                    Title = j.Title
                })
                .ToSearchResults(query);
        }
    }
}
