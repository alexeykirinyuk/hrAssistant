using System;

namespace HRAssistant.Web.Admin.Contracts.JobPositionContracts
{
    public sealed class SearchJobPositionItem
    {
        public Guid JobPositionId { get; set; }

        public string Title { get; set; }
    }
}
