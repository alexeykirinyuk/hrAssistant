using System.Collections.Generic;

namespace HRAssistant.Web.Domain
{
    public sealed class SelectQuestionSagaEntity : QuestionSagaEntity
    {
        public List<OptionEntity> SelectedOptions { get; set; }
    }
}
