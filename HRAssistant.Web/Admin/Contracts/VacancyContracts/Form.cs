using HRAssistant.Web.Admin.Contracts.Shared;

namespace HRAssistant.Web.Admin.Contracts.VacancyContracts
{
    public sealed class Form
    {
        public string Description { get; set; }

        public Question[] Questions { get; set; }
    }
}