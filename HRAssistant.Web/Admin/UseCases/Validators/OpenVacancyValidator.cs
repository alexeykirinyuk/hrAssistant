﻿using FluentValidation;using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.Admin.UseCases.Validators
{
    internal sealed class OpenVacancyValidator : AbstractValidator<OpenVacancy>
    {
        public OpenVacancyValidator(IVacancyRepository vacancyRepository)
        {
            RuleFor(m => m.VacancyId).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.VacancyId)
                        .MustAsync((id, token) => vacancyRepository.Exists(id.Value))
                        .WithMessage("Вакансии с id '{PropertyValue}' не существует.")
                        .MustAsync(async (id, token) =>
                            (await vacancyRepository.GetStatus(id.Value)) == VacancyStatusEntity.Draft)
                        .WithMessage("Вакансия должна быть черновиком.");
                });
        }
    }
}
