using System;
using System.Collections.Generic;

namespace HRAssistant.Web.Domain
{
    public sealed class JobPositionEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid TemplateId { get; set; }

        public TemplateEntity Template { get; set; }

        public List<VacancyEntity> Vacancies { get; set; }
    }
}
