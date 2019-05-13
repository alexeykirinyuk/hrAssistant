using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.JobPositionManagement
{
    public sealed class UpdateJobPosition : ICommand
    {
        public JobPosition JobPosition { get; set; }
    }
}
