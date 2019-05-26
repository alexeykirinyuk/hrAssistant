using HRAssistant.Web.Infrastructure;
using System;

namespace HRAssistant.Web.Contracts.InterviewList
{
    public sealed class SearchInterviews : SearchRequest<SearchInterviewsResult>
    {
        public Guid? JobPositionId { get; set; }

        public Guid? TeamId { get; set; }

        public string CandidateFullName { get; set; }

        public int? FromCorrectAnswersCount { get; set; }

        public int? ToCorrectAnswersCount { get; set; }
    }
}