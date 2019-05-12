using HRAssistant.Web.Admin.Contracts.Shared;

namespace HRAssistant.Web.Admin.Contracts.JobPositionContracts
{
    public sealed class GeneralQuestion : Question
    {
        public override QuestionType QuestionType => QuestionType.General;
    }
}
