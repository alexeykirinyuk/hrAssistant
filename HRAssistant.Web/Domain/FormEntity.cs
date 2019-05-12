using System;
using System.Collections.Generic;

namespace HRAssistant.Web.Domain
{
    public sealed class FormEntity
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public List<QuestionEntity> Questions { get; set; }

        public VacancyEntity Vacancy { get; set; }
    }
}