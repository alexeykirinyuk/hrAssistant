using System.Linq;
using FluentValidation;
using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    public sealed class SelectQuestionValidator : QuestionValidator<SelectQuestion>
    {
        public SelectQuestionValidator()
        {
            RuleFor(q => q.OneCorrectAnswer).NotNull().WithMessage(Messages.NotNull);
            RuleFor(q => q.Options).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleForEach(q => q.Options).SetValidator(new OptionValidator());

            RuleFor(q => q.Options)
                .Must(options => options.Any(option => option.IsCorrect))
                .WithMessage("Должен быть хотя бы один правильный ответ.");

            RuleFor(q => q.Options)
                .Must(options => options.Count(option => option.IsCorrect) < 2)
                .When(q => q.OneCorrectAnswer)
                .WithMessage("Если выставлена OneCorrectAnswer может быть только один правильный ответ.");
;        }
    }
}
