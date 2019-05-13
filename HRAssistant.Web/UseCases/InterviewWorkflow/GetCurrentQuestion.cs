using System;
using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    public sealed class GetCurrentQuestion
    {
        public Guid? InterviewId { get; set; }
    }
}
