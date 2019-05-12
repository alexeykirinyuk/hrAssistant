using FluentValidation;
using HRAssistant.Web.Admin.Contracts.JobPositionContracts;

namespace HRAssistant.Web.Admin.UseCases.Validators
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