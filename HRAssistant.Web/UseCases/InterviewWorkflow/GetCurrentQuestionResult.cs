using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    public sealed class GetCurrentQuestionResult
    {
        public Question Question { get; set; }
    }
}
