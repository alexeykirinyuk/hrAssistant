using System;

namespace HRAssistant.Web.Contracts.InterviewList
{
    public sealed class SearchInterviewItem
    {
        public Guid InterviewId { get; set; }

        public string JobPositionTitle { get; set; }

        public string TeamTitle { get; set; }

        public string CandidateFullName { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int IncorrectAnswersCount { get; set; }
    }
}