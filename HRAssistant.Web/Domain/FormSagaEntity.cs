using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAssistant.Web.Domain
{
    public sealed class FormSagaEntity
    {
        public Guid Id { get; set; }

        public Guid InterviewId { get; set; }

        public InterviewEntity Interview { get; set; }

        public List<QuestionSagaEntity> Questions { get; set; }
    }
}
