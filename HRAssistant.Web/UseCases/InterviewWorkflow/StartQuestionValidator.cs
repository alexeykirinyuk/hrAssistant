using FluentValidation;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class StartQuestionValidator : AbstractValidator<StartQuestion>
    {
        public StartQuestionValidator(IInterviewRepository interviewRepository)
        {
            RuleFor(m => m.InterviewId)
                .NotNull()
                .DependentRules(() =>
                {
                    RuleFor(m => m.InterviewId)
                        .MustAsync((id, token) => interviewRepository.Exists(id.Value))
                        .WithMessage("Интервью с Id '{PropertValue}' не сущетсвует.");
                });
        }
    }
}
