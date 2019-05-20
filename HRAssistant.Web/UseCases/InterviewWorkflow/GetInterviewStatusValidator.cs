using FluentValidation;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class GetInterviewStatusValidator : AbstractValidator<GetInterviewStatus>
    {
        public GetInterviewStatusValidator(IInterviewRepository interviewRepository)
        {
            RuleFor(g => g.InterviewId).NotNull().WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(g => g.InterviewId)
                        .MustAsync((id, _) => interviewRepository.Exists(id.Value))
                        .WithMessage(g => $"Интервью с Id '{g.InterviewId}' не существует.");
                });
        }
    }
}
