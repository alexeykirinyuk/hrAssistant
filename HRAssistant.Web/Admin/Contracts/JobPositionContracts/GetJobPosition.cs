using HRAssistant.Infrastructure.CQRS;
using System;

namespace HRAssistant.Admin.Contracts.JobPositionContracts
{
    public sealed class GetJobPosition : IQuery<GetJobPositionResult>
    {
        public Guid JobPositionId { get; set; }
    }
}
