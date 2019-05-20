using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class GetInterviewStatus : IQuery<GetInterviewStatusResult>
    {
        public Guid? InterviewId { get; set; }
    }
}
