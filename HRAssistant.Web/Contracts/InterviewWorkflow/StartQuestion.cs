using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class StartQuestion : ICommand<StartQuestionResult>
    {
        public Guid? InterviewId { get; set; }
    }
}
