using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.VacancyContracts
{
    public sealed class CreateVacancy : ICommand<CreateVacancyResult>
    {
        public Vacancy Vacancy { get; set; }
    }
}
