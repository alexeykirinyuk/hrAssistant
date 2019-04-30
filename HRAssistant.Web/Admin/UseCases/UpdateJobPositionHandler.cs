using HRAssistant.Admin.Contracts.JobPositionContracts;
using HRAssistant.DataAccess.Core;
using HRAssistant.Infrastructure.CQRS;
using LiteGuard;
using System.Threading.Tasks;

namespace HRAssistant.Admin.UseCases
{
    internal sealed class UpdateJobPositionHandler : ICommandHandler<UpdateJobPosition>
    {
        private readonly IJobPositionRepository _jobPositionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateJobPositionHandler(
            IJobPositionRepository jobPositionRepository,
            IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(jobPositionRepository), jobPositionRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _jobPositionRepository = jobPositionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateJobPosition request)
        {
            var entity = await _jobPositionRepository.Get(request.JobPosition.Id.Value);
            entity.Update(request.JobPosition);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
