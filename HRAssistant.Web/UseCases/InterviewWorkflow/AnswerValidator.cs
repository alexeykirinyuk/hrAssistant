using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class AnswerValidator : AbstractValidator<Answer>
    {
        public AnswerValidator(IInterviewRepository interviewRepository)
        {
            RuleFor(m => m.InterviewId).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(m => m.InterviewId)
                        .MustAsync((id, token) => interviewRepository.Exists(id.Value))
                        .WithMessage("Интервью с Id 'PropertyValue' не существет.");
                });

            RuleFor(m => m.Value)
                .NotEmpty()
                .When(m => m.Values == null)
                .WithMessage("Должно быть заполнено либо поле Value, либо Values.");

            RuleFor(m => m.Values)
                .NotNull()
                .When(m => string.IsNullOrEmpty(m.Value))
                .WithMessage("Должно быть заполнено либо поле Value, либо Values.");
        }
    }
}
