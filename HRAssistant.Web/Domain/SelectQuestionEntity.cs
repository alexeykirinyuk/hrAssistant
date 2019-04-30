using System;
using System.Collections.Generic;

namespace HRAssistant.Domain
{
    public sealed class SelectQuestionEntity : QuestionEntity
    {
        public List<OptionEntity> Options { get; set; }

        public bool OneCorrectAnswer { get; set; }
    }
}
