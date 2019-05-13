using System;

namespace HRAssistant.Web.Domain
{
    public sealed class InterviewEntity
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public CandidateEntity Candidate { get; set; }

        public DateTime? StartUtcTime { get; set; }

        public DateTime? EndUtcTime { get; set; }

        public InterviewStatusEntity Status { get; set; }
    }
}
