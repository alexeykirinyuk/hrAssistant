using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class Answer : ICommand<AnswerResult>
    {
        public Guid? InterviewId { get; set; }

        public string Value { get; set; }

        public Guid[] Values { get; set; }
    }
}
