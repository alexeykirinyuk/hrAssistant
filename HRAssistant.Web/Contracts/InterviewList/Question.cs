namespace HRAssistant.Web.Contracts.InterviewList
{
    public sealed class Question
    {
        public string Title { get; set; }

        public string Answer { get; set; }

        public bool? Result { get; set; }
    }
}