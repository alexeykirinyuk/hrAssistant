using FluentValidation;
using HRAssistant.Web.Contracts.CityManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.CityManagement
{
    internal sealed class UpdateCityValidator : AbstractValidator<UpdateCity>
    {
        public UpdateCityValidator(ICityRepository cityRepository)
        {
            RuleFor(m => m.City).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(m => m.City.Id).NotNull()
                        .DependentRules(() =>
                        {
                            RuleFor(m => m.City.Id)
                                .MustAsync((id, token) => cityRepository.Exists(id.Value));
                        });
                    RuleFor(m => m.City.Name).NotEmpty()
                        .DependentRules(() =>
                        {
                            RuleFor(m => m.City)
                                .MustAsync(async (city, token) => !await cityRepository.Exists(city.Name, city.Id.Value));
                        });
                });
        }
    }
}