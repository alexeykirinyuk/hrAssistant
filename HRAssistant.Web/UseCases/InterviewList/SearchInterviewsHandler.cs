using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.InterviewList;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.InterviewList
{
    internal sealed class SearchInterviewsHandler : IQueryHandler<SearchInterviews, SearchInterviewsResult>
    {
        private readonly IInterviewRepository _interviewRepository;

        public SearchInterviewsHandler(IInterviewRepository interviewRepository)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);

            _interviewRepository = interviewRepository;
        }

        public async Task<SearchInterviewsResult> Handle(SearchInterviews query)
        {
            return await _interviewRepository.Search()
                .FilterBy(query.JobPositionId, i => i.Vacancy.JobPositionId == query.JobPositionId)
                .FilterBy(query.TeamId, i => i.Vacancy.TeamId == query.TeamId)
                .FilterBy(query.CandidateFullName, i => (i.Candidate.FirstName + " " + i.Candidate.LastName).Contains(query.CandidateFullName))
                .FilterBy(query.FromCorrectAnswersCount, i => i.Result.CorrectAnswersCount > query.FromCorrectAnswersCount)
                .FilterBy(query.ToCorrectAnswersCount, i => i.Result.CorrectAnswersCount < query.ToCorrectAnswersCount)
                .Where(i => i.Status == InterviewStatusEntity.End)
                .Select(i => new SearchInterviewItem
                {
                    InterviewId = i.Id,
                    CandidateFullName = i.Candidate.FirstName + " " + i.Candidate.LastName,
                    CorrectAnswersCount = i.Result.CorrectAnswersCount,
                    IncorrectAnswersCount = i.Result.IncorrectAnswersCount,
                    TeamTitle = i.Vacancy.Team.Title,
                    JobPositionTitle = i.Vacancy.JobPosition.Title
                })
                .ToSearchResults(query);
        }
    }
}