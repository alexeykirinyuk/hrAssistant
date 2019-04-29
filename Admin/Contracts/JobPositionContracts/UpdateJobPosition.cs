using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Admin.Contracts.JobPositionContracts
{
    public sealed class UpdateJobPosition : ICommand
    {
        public JobPosition JobPosition { get; set; }
    }
}
