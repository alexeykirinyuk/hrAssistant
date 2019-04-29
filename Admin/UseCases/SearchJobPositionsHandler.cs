using HRAssistant.Admin.Contracts.JobPositionContracts;
using HRAssistant.DataAccess.Core;
using HRAssistant.Infrastructure;
using HRAssistant.Infrastructure.CQRS;
using LiteGuard;
using System.Linq;
using System.Threading.Tasks;

namespace HRAssistant.Admin.UseCases
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
