using System;

namespace HRAssistant.Web.Contracts.JobPositionManagement
{
    public sealed class SearchJobPositionItem
    {
        public Guid JobPositionId { get; set; }

        public string Title { get; set; }
    }
}
