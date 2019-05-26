using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.InterviewList
{
    public sealed class GetInterview : IQuery<GetInterviewResult>
    {
        public Guid? InterviewId { get; set; }
    }
}
