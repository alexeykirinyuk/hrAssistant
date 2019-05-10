using FluentValidation;
using HRAssistant.Web.Admin.Contracts.CityContracts;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.Admin.UseCases.Validators
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
