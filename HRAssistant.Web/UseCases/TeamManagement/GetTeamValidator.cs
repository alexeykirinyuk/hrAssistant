using FluentValidation;
using HRAssistant.Web.Contracts.TeamManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.TeamManagement
{
    internal sealed class GetTeamValidator : AbstractValidator<GetTeam>
    {
        public GetTeamValidator(ITeamRepository teamRepository)
        {
            RuleFor(m => m.TeamId).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.TeamId)
                        .MustAsync((id, token) => teamRepository.Exists(id.Value))
                        .WithMessage("Команды с Id '{PropertyValue}' не существует.");
                });
        }
    }
}
