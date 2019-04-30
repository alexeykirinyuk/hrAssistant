using System;

namespace HRAssistant.Domain
{
    public sealed class JobPositionEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid TemplateId { get; set; }

        public TemplateEntity Template { get; set; }
    }
}
