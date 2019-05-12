using FluentValidation;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.Admin.UseCases.Validators
{
    internal sealed class CloseVacancyValidator : AbstractValidator<CloseVacancy>
    {
        public CloseVacancyValidator(IVacancyRepository vacancyRepository)
        {
            RuleFor(m => m.VacancyId).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.VacancyId)
                        .MustAsync(async (id, token) =>
                            (await vacancyRepository.GetStatus(id.Value)) == VacancyStatusEntity.Opened)
                        .WithMessage("Для закрытия вакансия должна быть открыта.");
                });
        }
    }
}
