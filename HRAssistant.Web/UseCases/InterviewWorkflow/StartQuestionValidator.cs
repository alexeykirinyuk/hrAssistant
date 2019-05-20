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
                        .WithMessage(i => $"Интервью с Id '{i.InterviewId}' не сущетсвует.");

                    RuleFor(m => m.InterviewId)
                        .MustAsync((id, token) => interviewRepository.HasOpenQuestion(id.Value))
                        .WithMessage("Интервью не содержит открытых вопросов.");
                });
        }
    }
}
