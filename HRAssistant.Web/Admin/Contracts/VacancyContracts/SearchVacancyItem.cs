using System;

namespace HRAssistant.Web.Admin.Contracts.VacancyContracts
{
    public sealed class SearchVacancyItem
    {
        public Guid VacancyId { get; set; }

        public string TeamTitle { get; set; }

        public string JobPositionTitle { get; set; }

        public decimal? Salary { get; set; }

        public int JobsNumber { get; set; }

        public VacancyStatus Status { get; set; }
    }
}
