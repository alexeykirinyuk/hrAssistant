namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class GetVacancyResult
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int QuestionsCount { get; set; }
    }
}
