using System;

namespace HRAssistant.Web.Domain
{
    public abstract class QuestionEntity
    {
        public Guid Id { get; set; }

        public Guid? TemplateId { get; set; }

        public TemplateEntity Template { get; set; }

        public Guid? FormId { get; set; }

        public FormEntity Form { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int OrderIndex { get; set; }
    }
}
