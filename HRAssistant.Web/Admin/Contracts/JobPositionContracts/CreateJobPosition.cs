using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.JobPositionContracts
{
    public class CreateJobPosition : ICommand<CreateJobPositionResult>
    {
        public JobPosition JobPosition { get; set; }
    }
}
