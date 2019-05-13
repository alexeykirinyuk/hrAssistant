using Autofac;
using FluentValidation;
using HRAssistant.Web.Contracts.TeamManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.TeamManagement
{
    internal sealed class UpdateTeamValidator : AbstractValidator<UpdateTeam>
    {
        public UpdateTeamValidator(IComponentContext context, ITeamRepository teamRepository)
        {
            RuleFor(m => m.Team).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.Team.Id).NotNull()
                        .WithMessage(Messages.NotNull)
                        .MustAsync((id, token) => teamRepository.Exists(id.Value))
                        .WithMessage("Команды с ID '{PropertyValue}' не существует.");
                });
        }
    }
}
