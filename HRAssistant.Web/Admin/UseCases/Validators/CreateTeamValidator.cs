using Autofac;
using FluentValidation;
using HRAssistant.Web.Admin.Contracts.TeamContracts;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.Admin.UseCases.Validators
{
    internal sealed class CreateTeamValidator : AbstractValidator<CreateTeam>
    {
        public CreateTeamValidator(IComponentContext context, ITeamRepository teamRepository)
        {
            RuleFor(m => m.Team).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(m => m.Team.Id).Null()
                        .WithMessage("{PropertyName} не может быть задан.");

                    RuleFor(m => m.Team).SetValidator(context.Resolve<TeamValidator>())
                        .DependentRules(() =>
                        {
                            RuleFor(m => m.Team.Title)
                                .MustAsync(async (title, token) => !await teamRepository.Exists(title))
                                .WithMessage("Команда с названием '{PropertyValue}' уже существует.");
                        });

                });
        }
    }
}
