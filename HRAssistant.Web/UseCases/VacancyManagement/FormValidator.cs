using System;
using System.Linq;
using FluentValidation;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.Contracts.Shared;
using HRAssistant.Web.Contracts.VacancyManagement;
using HRAssistant.Web.UseCases.JobPositionManagement;

namespace HRAssistant.Web.UseCases.VacancyManagement
{
    internal sealed class FormValidator : AbstractValidator<Form>
    {
        public FormValidator()
        {
            RuleFor(form => form.Description).NotEmpty().WithMessage(Messages.NotEmpty);

            RuleForEach(form => form.Questions.OfType<SelectQuestion>())
                .SetValidator(new SelectQuestionValidator());
            
            RuleForEach(form => form.Questions.OfType<GeneralQuestion>())
                .SetValidator(new GeneralQuestionValidator());
            
            RuleForEach(form => form.Questions.OfType<InputQuestion>())
                .SetValidator(new InputQuestionValidator());
        }
    }
}
