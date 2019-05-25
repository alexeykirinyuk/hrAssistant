using System;

namespace HRAssistant.Web.Domain
{
    public sealed class InterviewEntity
    {
        public Guid Id { get; set; }

        public Guid VacancyId { get; set; }

        public VacancyEntity Vacancy { get; set; }

        public Guid CandidateId { get; set; }

        public CandidateEntity Candidate { get; set; }

        public Guid? FormSagaId { get; set; }

        public FormSagaEntity FormSagaEntity { get; set; }

        public InterviewStatusEntity Status { get; set; }

        public Guid ResultId { get; set; }

        public InterviewResultEntity Result { get; set; }
    }
}