using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class GetCurrentQuestionValidator : AbstractValidator<StartQuestion>
    {
        public GetCurrentQuestionValidator(IInterviewRepository interviewRepository)
        {
            RuleFor(m => m.InterviewId).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(m => m.InterviewId)
                        .MustAsync((id, token) => interviewRepository.Exists(id.Value))
                        .WithMessage("Интервью с Id '{PropertyValue}' не существует.");
                });
        }
    }
}
