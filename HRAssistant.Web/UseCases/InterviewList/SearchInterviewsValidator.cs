using FluentValidation;
using HRAssistant.Web.Contracts.InterviewList;
using HRAssistant.Web.DataAccess.Core;
using LiteGuard;

namespace HRAssistant.Web.UseCases.InterviewList
{
    internal sealed class SearchInterviewsValidator : AbstractValidator<SearchInterviews>
    {
        public SearchInterviewsValidator(IJobPositionRepository jobPositionRepository, ITeamRepository teamRepository)
        {
            Guard.AgainstNullArgument(nameof(jobPositionRepository), jobPositionRepository);
            Guard.AgainstNullArgument(nameof(teamRepository), teamRepository);

            RuleFor(s => s.JobPositionId)
                .MustAsync((id, token) => jobPositionRepository.Exists(id.Value))
                .When(s => s.JobPositionId.HasValue)
                .WithMessage(s => $"Должности с Id '{s.JobPositionId}' не существует.");
            
            RuleFor(s => s.TeamId)
                .MustAsync((id, token) => teamRepository.Exists(id.Value))
                .When(s => s.TeamId.HasValue)
                .WithMessage(s => $"Команды с Id '{s.JobPositionId}' не существует.");

            RuleFor(s => s.FromCorrectAnswersCount)
                .Must(number => number > 0)
                .When(s => s.FromCorrectAnswersCount.HasValue)
                .WithMessage("{PropertyName} не может быть меньше нуля.");

            RuleFor(s => s.ToCorrectAnswersCount)
                .Must(number => number > 0)
                .When(s => s.FromCorrectAnswersCount.HasValue)
                .WithMessage("{PropertyName} не может быть меньше нуля.");
        }
    }
}
