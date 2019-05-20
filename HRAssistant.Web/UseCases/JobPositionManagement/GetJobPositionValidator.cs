using FluentValidation;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    internal sealed class GetJobPositionValidator : AbstractValidator<GetJobPosition>
    {
        public GetJobPositionValidator(IJobPositionRepository jobPositionRepository)
        {
            RuleFor(g => g.JobPositionId)
                .NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(g => g.JobPositionId)
                        .MustAsync((id, token) => jobPositionRepository.Exists(id.Value))
                        .WithMessage(g => $"Должности с Id '{g.JobPositionId}' не существует.");
                });
        }
    }
}
