using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.VacancyContracts
{
    public sealed class CloseVacancy : ICommand
    {
        public Guid? VacancyId { get; set; }
    }
}
