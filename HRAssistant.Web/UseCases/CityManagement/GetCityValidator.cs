using FluentValidation;
using HRAssistant.Web.Contracts.CityManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.CityManagement
{
    internal sealed class GetCityValidator : AbstractValidator<GetCity>
    {
        public GetCityValidator(ICityRepository cityRepository)
        {
            RuleFor(m => m.CityId).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(m => m.CityId)
                        .MustAsync((id, token) => cityRepository.Exists(id.Value));
                });
        }
    }
}
