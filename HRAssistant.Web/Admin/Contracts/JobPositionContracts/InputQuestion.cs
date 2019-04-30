namespace HRAssistant.Admin.Contracts.JobPositionContracts
{
    public sealed class InputQuestion : Question
    {
        public string CorrectAnswer { get; set; }

        public override QuestionType QuestionType => QuestionType.Input;
    }
}
