using FluentValidation;
using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    public sealed class OptionValidator : AbstractValidator<Option>
    {
        public OptionValidator()
        {
            RuleFor(o => o.Title).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(o => o.IsCorrect).NotNull().WithMessage(Messages.NotNull);
        }
    }
}
