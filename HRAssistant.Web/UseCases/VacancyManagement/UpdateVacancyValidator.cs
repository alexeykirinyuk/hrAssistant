using Autofac;
using FluentValidation;
using HRAssistant.Web.Contracts.VacancyManagement;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.UseCases.VacancyManagement
{
    internal sealed class UpdateVacancyValidator : AbstractValidator<UpdateVacancy>
    {
        public UpdateVacancyValidator(
            IVacancyRepository vacancyRepository,
            IComponentContext componentContext)
        {
            Guard.AgainstNullArgument(nameof(vacancyRepository), vacancyRepository);
            Guard.AgainstNullArgument(nameof(componentContext), componentContext);

            RuleFor(m => m.Vacancy).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.Vacancy.Id).NotNull()
                        .WithMessage(Messages.NotNull)
                        .DependentRules(() =>
                        {
                            RuleFor(m => m.Vacancy.Id)
                                .MustAsync((id, token) => vacancyRepository.Exists(id.Value))
                                .WithMessage("Вакансии с id '{PropertyValue}' не существует.");

                            RuleFor(m => m.Vacancy)
                                .MustAsync(async (vacancy, token) =>
                                    (await vacancyRepository.GetStatus(vacancy.Id.Value)) == VacancyStatusEntity.Draft)
                                .WithMessage("Для изменения вакансия должна быть черновиком.")

                                .MustAsync(async (vacancy, token) =>
                                    (await vacancyRepository.Get(vacancy.Id.Value)).JobPositionId == vacancy.JobPositionId)
                                .WithMessage("Нельзя изменять должность вакансии.");
                            ;
                        });

                    RuleFor(m => m.Vacancy)
                        .SetValidator(componentContext.Resolve<VacancyValidator>());
                });
        }
    }
}