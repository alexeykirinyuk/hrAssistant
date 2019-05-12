using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.VacancyContracts
{
    public sealed class GetVacancy : IQuery<GetVacancyResult>
    {
        public Guid? VacancyId { get; set; }
    }
}
