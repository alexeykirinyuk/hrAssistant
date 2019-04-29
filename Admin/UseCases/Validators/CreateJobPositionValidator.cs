using FluentValidation;
using HRAssistant.Admin.Contracts.JobPositionContracts;
using System.Collections.Generic;

namespace HRAssistant.Admin.UseCases.Validators
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