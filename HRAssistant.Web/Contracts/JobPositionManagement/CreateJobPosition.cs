using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.JobPositionManagement
{
    public class CreateJobPosition : ICommand<CreateJobPositionResult>
    {
        public JobPosition JobPosition { get; set; }
    }
}
