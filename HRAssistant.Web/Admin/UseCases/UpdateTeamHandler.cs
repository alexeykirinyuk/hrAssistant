using System.Threading.Tasks;
using HRAssistant.DataAccess.Core;
using HRAssistant.Infrastructure.CQRS;
using HRAssistant.Web.Admin.Contracts.TeamContracts;
using HRAssistant.Web.DataAccess.Core;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class UpdateTeamHandler : ICommandHandler<UpdateTeam>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeamHandler(ITeamRepository teamRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(teamRepository), teamRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateTeam request)
        {
            var entity = await _teamRepository.Get(request.Team.Id.Value);
            entity.Update(request.Team);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
