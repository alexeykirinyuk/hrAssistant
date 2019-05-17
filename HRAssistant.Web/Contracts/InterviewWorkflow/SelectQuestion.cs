using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class SelectQuestion : Question
    {
        public Option[] Options { get; set; }

        public bool OneCorrectAnswer { get; set; }

        public override QuestionType QuestionType => QuestionType.Select;
    }
}
