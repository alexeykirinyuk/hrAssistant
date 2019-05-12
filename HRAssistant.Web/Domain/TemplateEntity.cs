using System;
using System.Collections.Generic;

namespace HRAssistant.Web.Domain
{
    public sealed class TemplateEntity
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public List<QuestionEntity> Questions { get; set; }

        public JobPositionEntity JobPosition { get; set; }
    }
}
