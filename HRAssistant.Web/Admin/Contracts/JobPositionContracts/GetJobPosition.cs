using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.JobPositionContracts
{
    public sealed class GetJobPosition : IQuery<GetJobPositionResult>
    {
        public Guid JobPositionId { get; set; }
    }
}
