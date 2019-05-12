using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.JobPositionContracts;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class CreateJobPositionHandler : ICommandHandler<CreateJobPosition, CreateJobPositionResult>
    {
        private readonly IJobPositionRepository _jobPositionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateJobPositionHandler(
            IJobPositionRepository jobPositionRepository,
            IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(jobPositionRepository), jobPositionRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _jobPositionRepository = jobPositionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateJobPositionResult> Handle(CreateJobPosition command)
        {
            var jobPositionId = Guid.NewGuid();
            var entity = new JobPositionEntity
            {
                Id = jobPositionId,
                Template = new TemplateEntity
                {
                    Questions = new List<QuestionEntity>()
                }
            };
            entity.Update(command.JobPosition);
            _jobPositionRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync();

            return new CreateJobPositionResult
            {
                JobPositionId = jobPositionId
            };
        }
    }
}
