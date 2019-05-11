using System;
using System.Threading.Tasks;
using HRAssistant.DataAccess.Core;
using HRAssistant.Infrastructure.CQRS;
using HRAssistant.Web.Admin.Contracts.TeamContracts;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class CreateTeamHandler : ICommandHandler<CreateTeam, CreateTeamResult>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeamHandler(
            ITeamRepository teamRepository,
            IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(teamRepository), teamRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateTeamResult> Handle(CreateTeam command)
        {
            var teamId = Guid.NewGuid();

            var teamEntity = new TeamEntity { Id = teamId };
            teamEntity.Update(command.Team);

            _teamRepository.Add(teamEntity);

            await _unitOfWork.SaveChangesAsync();

            return new CreateTeamResult {TeamId = teamId};
        }
    }
}
