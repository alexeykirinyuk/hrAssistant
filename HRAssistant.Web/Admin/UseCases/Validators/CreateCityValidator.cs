using FluentValidation;
using HRAssistant.Web.Admin.Contracts.CityContracts;

namespace HRAssistant.Web.Admin.UseCases.Validators
{
    public sealed class CreateCityValidator : AbstractValidator<CreateCity>
    {
        public CreateCityValidator()
        {
            RuleFor(m => m.City).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(m => m.City.Id).Null();
                    RuleFor(m => m.City.Name).NotEmpty();
                });
        }
    }
}
