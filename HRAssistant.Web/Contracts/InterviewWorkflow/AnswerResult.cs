namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class AnswerResult
    {
        public bool HasQuestions { get; set; }

        public bool? Result { get; set; }
    }
}