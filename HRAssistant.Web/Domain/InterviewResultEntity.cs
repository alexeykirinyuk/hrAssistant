using System;

namespace HRAssistant.Web.Domain
{
    public sealed class InterviewResultEntity
    {
        public Guid Id { get; set; }

        public InterviewEntity Interview { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int IncorrectAnswersCount { get; set; }
    }
}