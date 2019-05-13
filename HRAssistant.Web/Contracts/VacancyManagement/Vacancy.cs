using System;

namespace HRAssistant.Web.Contracts.VacancyManagement
{
    public sealed class Vacancy
    {
        public Guid? Id { get; set; }

        public Guid? TeamId { get; set; }

        public Guid? JobPositionId { get; set; }

        public decimal? Salary { get; set; }

        public string CandidateRequirements { get; set; }

        public int? JobsNumber { get; set; }

        public Form Form { get; set; }

        public VacancyStatus? Status { get; set; }
    }
}
