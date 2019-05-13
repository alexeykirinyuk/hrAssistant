using FluentValidation;
using HRAssistant.Web.Contracts.CityManagement;

namespace HRAssistant.Web.UseCases.CityManagement
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
