using Autofac;
using FluentValidation;
using HRAssistant.Web.Admin.Contracts.VacancyContracts;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases.Validators
{
    internal sealed class CreateVacancyValidator : AbstractValidator<CreateVacancy>
    {
        public CreateVacancyValidator(IComponentContext context)
        {
            Guard.AgainstNullArgument(nameof(context), context);

            RuleFor(m => m.Vacancy).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.Vacancy.Id).Null()
                        .WithMessage("{PropertyName} не может быть задано.");

                    RuleFor(m => m.Vacancy)
                        .SetValidator(context.Resolve<VacancyValidator>());
                });
        }
    }
}
