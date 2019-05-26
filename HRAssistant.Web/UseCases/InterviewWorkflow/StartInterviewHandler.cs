using System;
using System.Linq;
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
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StartInterviewHandler(
            IInterviewRepository interviewRepository,
            IVacancyRepository vacancyRepository,
            IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);
            Guard.AgainstNullArgument(nameof(vacancyRepository), vacancyRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _interviewRepository = interviewRepository;
            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(StartInterview request)
        {
            var interview = await _interviewRepository.Get(request.InterviewId.Value);
            var vacancy = await _vacancyRepository.Get(interview.VacancyId);

            interview.Status = InterviewStatusEntity.Started;
            interview.FormSagaEntity = new FormSagaEntity
            {
                Questions = vacancy.Form.Questions
                    .Select(question =>
                    {
                        QuestionSagaEntity saga;
                        switch (question)
                        {
                            case InputQuestionEntity input:
                                saga = new InputQuestionSagaEntity();
                                break;
                            case SelectQuestionEntity select:
                                saga = new SelectQuestionSagaEntity();
                                break;
                            case GeneralQuestionEntity general:
                                saga = new GeneralQuestionSagaEntity();
                                break;
                            default:
                                throw new InvalidOperationException($"Can't convert '{question.GetType().FullName}'");
                        }

                        saga.QuestionId = question.Id;
                        saga.Status = QuestionSagaStatusEntity.NotStarted;

                        return saga;
                    })
                    .ToList()
            };

            await _unitOfWork.SaveChangesAsync();
        }
    }
}