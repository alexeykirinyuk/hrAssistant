using FluentValidation;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.Admin.UseCases.Validators
{
    internal sealed class CreateVacancyValidator : AbstractValidator<CreateVacancy>
    {
        public CreateVacancyValidator(ITeamRepository teamRepository, IJobPositionRepository jobPositionRepository)
        {
            RuleFor(m => m.Vacancy).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.Vacancy.Id).Null()
                        .WithMessage("{PropertyName} не может быть задано.");

                    RuleFor(m => m.Vacancy.Form).NotNull()
                        .WithMessage(Messages.NotNull);

                    RuleFor(m => m.Vacancy.JobPositionId).NotNull()
                        .WithMessage(Messages.NotNull)
                        .DependentRules(() =>
                        {
                            RuleFor(m => m.Vacancy.JobPositionId.Value)
                                .MustAsync((id, token) => jobPositionRepository.Exists(id))
                                .WithMessage("Должности с Id '{PropertyValue}' не существует.");
                        });

                    RuleFor(m => m.Vacancy.TeamId).NotNull()
                        .WithMessage(Messages.NotNull)
                        .DependentRules(() =>
                        {
                            RuleFor(m => m.Vacancy.TeamId.Value)
                                .MustAsync((id, token) => teamRepository.Exists(id))
                                .WithMessage("Команды с Id '{PropertyValue}' не существует.");
                        });;

                    RuleFor(m => m.Vacancy.JobsNumber).NotNull()
                        .WithMessage(Messages.NotNull);
                });
        }
    }
}
