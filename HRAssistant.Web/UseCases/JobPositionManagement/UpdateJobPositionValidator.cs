using FluentValidation;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    internal sealed class UpdateJobPositionValidator : AbstractValidator<UpdateJobPosition>
    {
        public UpdateJobPositionValidator(IJobPositionRepository jobPositionRepository)
        {
            RuleFor(u => u.JobPosition).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(u => u.JobPosition.Id)
                        .NotNull()
                        .WithMessage(Messages.NotNull)
                        .DependentRules(() =>
                        {
                            RuleFor(u => u.JobPosition.Id)
                                .MustAsync((id, token) => jobPositionRepository.Exists(id.Value))
                                .WithMessage("Должности с Id '{PropertyValue}' не существуют.");
                        });

                    RuleFor(u => u.JobPosition).SetValidator(new JobPositionValidator())
                        .DependentRules(() =>
                        {
                            RuleFor(u => u.JobPosition)
                                .MustAsync(async (jobPosition, token) => !(await jobPositionRepository.Exists(jobPosition.Title, jobPosition.Id)))
                                .WithMessage(u => $"Должность с таким названием '{u.JobPosition.Title}' уже существует.");
                        });
                });
        }
    }
}
