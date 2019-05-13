using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.JobPositionManagement
{
    public sealed class GetJobPosition : IQuery<GetJobPositionResult>
    {
        public Guid JobPositionId { get; set; }
    }
}
