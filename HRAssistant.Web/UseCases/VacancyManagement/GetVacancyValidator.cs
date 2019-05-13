using FluentValidation;
using HRAssistant.Web.Contracts.VacancyManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.VacancyManagement
{
    internal sealed class GetVacancyValidator : AbstractValidator<GetVacancy>
    {
        public GetVacancyValidator(IVacancyRepository vacancyRepository)
        {
            RuleFor(m => m.VacancyId).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.VacancyId)
                        .MustAsync((id, token) => vacancyRepository.Exists(id.Value));
                });
        }
    }
}
