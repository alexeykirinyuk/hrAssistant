using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.VacancyManagement
{
    public sealed class OpenVacancy : ICommand
    {
        public Guid? VacancyId { get; set; }
    }
}
