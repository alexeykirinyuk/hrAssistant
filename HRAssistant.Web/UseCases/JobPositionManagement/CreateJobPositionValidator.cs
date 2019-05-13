using FluentValidation;
using HRAssistant.Web.Contracts.JobPositionManagement;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    internal sealed class CreateJobPositionValidator : AbstractValidator<CreateJobPosition>
    {
        public CreateJobPositionValidator()
        {
            RuleFor(p => p.JobPosition).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(p => p.JobPosition.Id).Null();
                    RuleFor(p => p.JobPosition).SetValidator(new JobPositionValidator());
                });
        }
    }
}