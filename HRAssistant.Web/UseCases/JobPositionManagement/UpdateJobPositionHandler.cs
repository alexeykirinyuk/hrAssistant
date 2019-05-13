using System.Threading.Tasks;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.JobPositionManagement
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
