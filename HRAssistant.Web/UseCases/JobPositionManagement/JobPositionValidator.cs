using System.Linq;
using FluentValidation;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    public sealed class JobPositionValidator : AbstractValidator<JobPosition>
    {
        public JobPositionValidator()
        {
            RuleFor(p => p.Title).NotNull().NotEmpty();
            RuleFor(p => p.Template).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(t => t.Template.Description).NotEmpty().WithMessage(Messages.NotEmpty);

                    RuleForEach(t => t.Template.Questions.OfType<SelectQuestion>())
                        .SetValidator(new SelectQuestionValidator());

                    RuleForEach(t => t.Template.Questions.OfType<GeneralQuestion>())
                        .SetValidator(new GeneralQuestionValidator());

                    RuleForEach(t => t.Template.Questions.OfType<InputQuestion>())
                        .SetValidator(new InputQuestionValidator());

                    RuleFor(t => t.Template.Questions.Select((question, index) => new {question, index}).ToArray())
                        .Must(items => items.Any(item => !items.Any(i => i.question.OrderIndex.Value == item.question.OrderIndex && i.index != item.index)))
                        .WithMessage("OrderIndex не могут совпадать у двух вопросов.");
                });
        }
    }
}
