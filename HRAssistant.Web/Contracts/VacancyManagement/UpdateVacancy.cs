using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.VacancyManagement
{
    public sealed class UpdateVacancy : ICommand
    {
        public Vacancy Vacancy { get; set; }
    }
}