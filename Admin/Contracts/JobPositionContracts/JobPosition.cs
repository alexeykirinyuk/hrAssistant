using System;

namespace HRAssistant.Admin.Contracts.JobPositionContracts
{
    public sealed class JobPosition
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public Template Template { get; set; }
    }
}
