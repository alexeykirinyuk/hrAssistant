using FluentValidation;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    internal sealed class CreateJobPositionValidator : AbstractValidator<CreateJobPosition>
    {
        public CreateJobPositionValidator(IJobPositionRepository jobPositionRepository)
        {
            RuleFor(p => p.JobPosition).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(p => p.JobPosition.Id).Null();
                    RuleFor(p => p.JobPosition).SetValidator(new JobPositionValidator())
                        .DependentRules(() =>
                        {
                            RuleFor(c => c.JobPosition.Title)
                                .MustAsync(async (title, token) => !(await jobPositionRepository.Exists(title)))
                                .WithMessage("Нельзя добавить должность с таким же именем.");
                        });
                });
        }
    }
}