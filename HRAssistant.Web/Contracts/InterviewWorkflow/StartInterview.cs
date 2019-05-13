using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class StartInterview : ICommand
    {
        public Guid? InterviewId { get; set; }
    }
}
