using System;

namespace HRAssistant.Domain
{
    public sealed class OptionEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid QuestionId { get; set; }

        public SelectQuestionEntity Question { get; set; }

        public bool IsCorrect { get; set; }
    }
}
