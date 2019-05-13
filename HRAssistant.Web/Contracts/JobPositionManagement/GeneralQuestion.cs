using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.Contracts.JobPositionManagement
{
    public sealed class GeneralQuestion : Question
    {
        public override QuestionType QuestionType => QuestionType.General;
    }
}
