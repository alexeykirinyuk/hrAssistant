using FluentValidation;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.Admin.UseCases.Validators
{
    internal sealed class VacancyValidator : AbstractValidator<Vacancy>
    {
        public VacancyValidator(IJobPositionRepository jobPositionRepository, ITeamRepository teamRepository)
        {
            RuleFor(m => m.Form).NotNull()
                .WithMessage(Messages.NotNull);

            RuleFor(m => m.Status).Null()
                .WithMessage("Нельзя задавать статус вакансии.");

            RuleFor(m => m.JobPositionId).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.JobPositionId.Value)
                        .MustAsync((id, token) => jobPositionRepository.Exists(id))
                        .WithMessage("Должности с Id '{PropertyValue}' не существует.");
                });

            RuleFor(m => m.TeamId).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.TeamId.Value)
                        .MustAsync((id, token) => teamRepository.Exists(id))
                        .WithMessage("Команды с Id '{PropertyValue}' не существует.");
                });
            ;

            RuleFor(m => m.JobsNumber).NotNull()
                .WithMessage(Messages.NotNull);
        }
    }
}