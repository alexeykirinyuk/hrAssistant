using FluentValidation;
using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    public abstract class QuestionValidator<T> : AbstractValidator<T>
        where T : Question
    {
        protected QuestionValidator()
        {
            RuleFor(q => q.Title).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(q => q.Description).NotEmpty().WithMessage(Messages.NotEmpty);

            
            RuleFor(q => q.OrderIndex).NotNull().WithMessage(Messages.NotNull)
                .Must(index => index >= 0).WithMessage("{PropertyName} должен быть больше или равен нулю.");

            RuleFor(q => q.MaxAnswerSeconds)
                .NotNull().WithMessage(Messages.NotNull)
                .Must(seconds => seconds > 0).WithMessage("Количество секунд должно быть больше нуля.");
        }
    }
}