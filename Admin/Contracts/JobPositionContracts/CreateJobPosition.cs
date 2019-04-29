using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Admin.Contracts.JobPositionContracts
{
    public class CreateJobPosition : ICommand<CreateJobPositionResult>
    {
        public JobPosition JobPosition { get; set; }
    }
}
