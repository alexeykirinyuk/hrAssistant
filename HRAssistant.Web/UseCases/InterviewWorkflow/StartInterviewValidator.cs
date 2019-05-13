using System;
using System.Threading.Tasks;
using FluentValidation;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class StartInterviewValidator : AbstractValidator<StartInterview>
    {
        private readonly IInterviewRepository _interviewRepository;

        public StartInterviewValidator(IInterviewRepository interviewRepository)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);

            _interviewRepository = interviewRepository;

            RuleFor(m => m.InterviewId).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.InterviewId)
                        .MustAsync((id, token) => interviewRepository.Exists(id.Value))
                        .WithMessage("Такого интервью не существует.");

                    RuleFor(m => m.InterviewId)
                        .MustAsync((id, token) => CanStartInterview(id.Value))
                        .WithMessage("Интервью нельзя начать.");
                });
        }

        private async Task<bool> CanStartInterview(Guid interviewId)
        {
            var interview = await _interviewRepository.Get(interviewId);

            return interview.Status == InterviewStatusEntity.ContactInformationInitialized;
        }
    }
}
