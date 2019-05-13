using System;
using HRAssistant.Web.Infrastructure;

namespace HRAssistant.Web.Contracts.VacancyManagement
{
    public class SearchVacancies : SearchRequest<SearchVacanciesResult>
    {
        public VacancyStatus[] Statuses { get; set; }

        public Guid? TeamId { get; set; }

        public Guid? JobPositionId { get; set; }
    }
}
