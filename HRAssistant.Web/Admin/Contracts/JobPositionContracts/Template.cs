using HRAssistant.Web.Admin.Contracts.Shared;

namespace HRAssistant.Web.Admin.Contracts.JobPositionContracts
{
    public sealed class Template
    {
        public string Description { get; set; }

        public Question[] Questions { get; set; }
    }
}
