using FluentValidation;
using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    public sealed class InputQuestionValidator : QuestionValidator<InputQuestion>
    {
        public InputQuestionValidator()
        {
            RuleFor(q => q.CorrectAnswer).NotEmpty().WithMessage(Messages.NotEmpty);
        }
    }
}
