using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.VacancyContracts
{
    public sealed class UpdateVacancy : ICommand
    {
        public Vacancy Vacancy { get; set; }
    }
}