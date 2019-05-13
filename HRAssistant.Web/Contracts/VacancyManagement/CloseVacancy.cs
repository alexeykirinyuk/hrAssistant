using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.VacancyManagement
{
    public sealed class CloseVacancy : ICommand
    {
        public Guid? VacancyId { get; set; }
    }
}
