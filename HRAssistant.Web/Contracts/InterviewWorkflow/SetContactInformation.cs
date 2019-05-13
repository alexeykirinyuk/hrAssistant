using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class SetContactInformation : ICommand<SetContactInformationResult>
    {
        public Guid? VacancyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
