using System;
using System.Collections.Generic;

namespace HRAssistant.Web.Domain
{
    public sealed class VacancyEntity
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        public TeamEntity Team { get; set; }

        public Guid JobPositionId { get; set; }

        public JobPositionEntity JobPosition { get; set; }

        public decimal? Salary { get; set; }

        public string CandidateRequirements { get; set; }

        public int JobsNumber { get; set; }

        public Guid FormId { get; set; }
        
        public FormEntity Form { get; set; }

        public VacancyStatusEntity Status { get; set; }

        public List<InterviewEntity> Interviews { get; set; }
    }
}
