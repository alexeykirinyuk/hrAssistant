using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.Contracts.JobPositionManagement
{
    public sealed class Template
    {
        public string Description { get; set; }

        public Question[] Questions { get; set; }
    }
}
