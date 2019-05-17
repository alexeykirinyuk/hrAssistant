using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAssistant.Web.Domain
{
    public abstract class QuestionSagaEntity
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public QuestionEntity Question { get; set; }

        public Guid? FormSagaId { get; set; }

        public FormSagaEntity FormSaga { get; set; }

        public DateTime? StartUtcTime { get; set; }

        public DateTime? EndUtcTime { get; set; }

        public QuestionSagaStatusEntity Status { get; set; }

        public bool? Result { get; set; }
    }
}
