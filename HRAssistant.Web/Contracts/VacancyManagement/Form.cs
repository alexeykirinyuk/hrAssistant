using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.Contracts.VacancyManagement
{
    public sealed class Form
    {
        public string Description { get; set; }

        public Question[] Questions { get; set; }
    }
}