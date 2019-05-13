using System;

namespace HRAssistant.Web.Contracts.VacancyManagement
{
    public sealed class SearchVacancyItem
    {
        public Guid VacancyId { get; set; }

        public Guid TeamId { get; set; }

        public string TeamTitle { get; set; }

        public Guid JobPositionId { get; set; }

        public string JobPositionTitle { get; set; }

        public decimal? Salary { get; set; }

        public int JobsNumber { get; set; }

        public VacancyStatus Status { get; set; }
    }
}
