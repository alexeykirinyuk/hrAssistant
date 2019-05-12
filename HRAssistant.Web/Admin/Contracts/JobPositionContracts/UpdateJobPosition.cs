using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.JobPositionContracts
{
    public sealed class UpdateJobPosition : ICommand
    {
        public JobPosition JobPosition { get; set; }
    }
}
