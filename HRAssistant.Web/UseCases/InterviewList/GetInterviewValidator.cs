using FluentValidation;
using HRAssistant.Web.Contracts.InterviewList;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.UseCases.InterviewList
{
    internal sealed class GetInterviewValidator : AbstractValidator<GetInterview>
    {
        public GetInterviewValidator(IInterviewRepository interviewRepository)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);

            RuleFor(i => i.InterviewId)
                .NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(i => i.InterviewId)
                        .MustAsync((id, token) => interviewRepository.Exists(id.Value))
                        .WithMessage("Интервью с Id '{PropertyValue}' не найдено.")
                        .DependentRules(() =>
                        {
                            RuleFor(i => i.InterviewId)
                                .MustAsync(async (id, token) => (await interviewRepository.Get(id.Value)).Status == InterviewStatusEntity.End)
                                .WithMessage("Нельзя просматривать незаконченное интервью.");
                        });
                });
        }
    }
}