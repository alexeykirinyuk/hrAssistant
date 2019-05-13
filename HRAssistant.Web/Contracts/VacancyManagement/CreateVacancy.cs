using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.VacancyManagement
{
    public sealed class CreateVacancy : ICommand<CreateVacancyResult>
    {
        public Vacancy Vacancy { get; set; }
    }
}
