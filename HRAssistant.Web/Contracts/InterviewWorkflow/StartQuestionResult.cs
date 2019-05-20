using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class StartQuestionResult
    {
        public Question Question { get; set; }

        public int Index { get; set; }

        public int TotalCount { get; set; }
    }
}
