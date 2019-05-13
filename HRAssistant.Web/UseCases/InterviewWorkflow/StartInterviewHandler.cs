using System;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class StartInterviewHandler : ICommandHandler<StartInterview>
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StartInterviewHandler(IInterviewRepository interviewRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _interviewRepository = interviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(StartInterview request)
        {
            var interview = await _interviewRepository.Get(request.InterviewId.Value);

            interview.StartUtcTime = DateTime.UtcNow;
            interview.Status = InterviewStatusEntity.Started;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
