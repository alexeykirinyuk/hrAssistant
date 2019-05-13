using FluentValidation;
using HRAssistant.Web.Contracts.TeamManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.TeamManagement
{
    internal sealed class TeamValidator : AbstractValidator<Team>
    {
        public TeamValidator(ICityRepository cityRepository, IUserRepository userRepository)
        {
            RuleFor(m => m.Title).NotEmpty()
                .WithMessage("Поле {PropertyName} должно быть задано.");

            RuleFor(m => m.CityId).NotNull()
                .WithMessage("{PropertyName} должен быть задан.")
                .DependentRules(() =>
                {
                    RuleFor(m => m.CityId.Value)
                        .MustAsync((cityId, token) => cityRepository.Exists(cityId))
                        .WithMessage("Города с Id '{PropertyValue}' не существует.");
                });

            RuleFor(m => m.TeamLeadId).NotNull()
                .WithMessage("{PropertyName} должен быть задан.")
                .DependentRules(() =>
                {
                    RuleFor(m => m.TeamLeadId.Value)
                        .MustAsync((userId, token) => userRepository.Exists(userId))
                        .WithMessage("Пользователя с Id '{PropertyValue}' не существует.");
                });

            RuleFor(m => m.IsBlocked).NotNull()
                .WithMessage("{PropertyName} должен быть задан.");
        }
    }
}
