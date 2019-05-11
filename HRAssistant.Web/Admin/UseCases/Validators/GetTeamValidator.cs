using FluentValidation;
using HRAssistant.Web.Admin.Contracts.TeamContracts;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.Admin.UseCases.Validators
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
