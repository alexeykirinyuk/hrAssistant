using System;
using HRAssistant.Web.Admin.Contracts.Shared;

namespace HRAssistant.Web.Admin.Contracts.JobPositionContracts
{
    public sealed class JobPosition
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public Template Template { get; set; }
    }
}
