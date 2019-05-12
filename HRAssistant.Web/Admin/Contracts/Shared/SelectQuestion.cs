namespace HRAssistant.Web.Admin.Contracts.Shared
{
    public sealed class SelectQuestion : Question
    {
        public Option[] Options { get; set; }

        public bool OneCorrectAnswer { get; set; }

        public override QuestionType QuestionType => QuestionType.Select;
    }
}
