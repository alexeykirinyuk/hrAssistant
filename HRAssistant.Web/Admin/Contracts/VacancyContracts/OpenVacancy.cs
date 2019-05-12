using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.VacancyContracts
{
    public sealed class OpenVacancy : ICommand
    {
        public Guid? VacancyId { get; set; }
    }
}
