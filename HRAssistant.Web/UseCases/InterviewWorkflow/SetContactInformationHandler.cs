using System;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class SetContactInformationHandler : ICommandHandler<SetContactInformation, SetContactInformationResult>
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetContactInformationHandler(IInterviewRepository interviewRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _interviewRepository = interviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SetContactInformationResult> Handle(SetContactInformation command)
        {
            var interviewId = Guid.NewGuid();

            _interviewRepository.Add(new InterviewEntity
            {
                Id = interviewId,
                VacancyId = command.VacancyId.Value,
                Candidate = new CandidateEntity
                {
                    Email = command.Email,
                    Phone = command.Phone,
                    FirstName = command.FirstName,
                    LastName = command.LastName
                },
                Status = InterviewStatusEntity.ContactInformationInitialized
            });

            await _unitOfWork.SaveChangesAsync();

            return new SetContactInformationResult
            {
                InterviewId = interviewId
            };
        }
    }
}
