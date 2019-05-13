using System;

namespace HRAssistant.Web.Contracts.JobPositionManagement
{
    public sealed class JobPosition
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public Template Template { get; set; }
    }
}
