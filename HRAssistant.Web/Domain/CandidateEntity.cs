using System;

namespace HRAssistant.Web.Domain
{
    public sealed class CandidateEntity
    {
        public Guid Id { get;set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public InterviewEntity Interview { get; set; }
    }
}
