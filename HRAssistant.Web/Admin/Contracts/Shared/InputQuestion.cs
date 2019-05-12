namespace HRAssistant.Web.Admin.Contracts.Shared
{
    public sealed class InputQuestion : Question
    {
        public string CorrectAnswer { get; set; }

        public override QuestionType QuestionType => QuestionType.Input;
    }
}
