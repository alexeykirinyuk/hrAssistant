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
    internal sealed class AnswerHandler : ICommandHandler<Answer, AnswerResult>
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AnswerHandler(IInterviewRepository interviewRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _interviewRepository = interviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AnswerResult> Handle(Answer command)
        {
            var interview = await _interviewRepository.Get(command.InterviewId.Value);
            var currentQuestion = interview.GetQuestions().GetCurrent();

            switch (currentQuestion)
            {
                case GeneralQuestionSagaEntity generalQuestionSagaEntity:
                    Answer(command, generalQuestionSagaEntity);
                    break;
                case InputQuestionSagaEntity inputQuestionSagaEntity:
                    Answer(command, inputQuestionSagaEntity);
                    break;
                case SelectQuestionSagaEntity selectQuestionSagaEntity:
                    Answer(command, selectQuestionSagaEntity, currentQuestion);
                    break;
                default:
                    throw new InvalidOperationException("Can't set up question.");
            }

            currentQuestion.EndUtcTime = DateTime.UtcNow;
            currentQuestion.Status = QuestionSagaStatusEntity.Answered;

            var hasQuestions = interview.GetQuestions().Any(question => question.IsOpen());
            if (!hasQuestions)
            {
                interview.Status = InterviewStatusEntity.End;
                interview.Result = new InterviewResultEntity
                {
                    CorrectAnswersCount = interview.GetQuestions()
                        .Count(q => (q.Result.HasValue && q.Result.Value)),
                    IncorrectAnswersCount = interview.GetQuestions()
                        .Count(q => q.Result.HasValue && !q.Result.Value)
                };
            }

            await _unitOfWork.SaveChangesAsync();

            return new AnswerResult
            {
                HasQuestions = hasQuestions,
                Result = currentQuestion.Result
            };
        }

        private static void Answer(Answer command, GeneralQuestionSagaEntity generalQuestionSagaEntity)
        {
            generalQuestionSagaEntity.Answer = command.Value;
            generalQuestionSagaEntity.Result = null;
        }

        private static void Answer(Answer command, InputQuestionSagaEntity inputQuestionSagaEntity)
        {
            inputQuestionSagaEntity.Answer = command.Value;
            inputQuestionSagaEntity.Result = string.Equals(
                ((InputQuestionEntity)inputQuestionSagaEntity.Question).CorrectAnswer,
                command.Value,
                StringComparison.InvariantCultureIgnoreCase);
        }

        private static void Answer(Answer command, SelectQuestionSagaEntity selectQuestionSagaEntity,
            QuestionSagaEntity currentQuestion)
        {
            selectQuestionSagaEntity.SelectedOptions = command.Values.Select(value =>
                    ((SelectQuestionEntity) currentQuestion.Question)
                    .Options
                    .Single(option => option.Id == value))
                .ToList();

            selectQuestionSagaEntity.Result = selectQuestionSagaEntity
                .SelectedOptions
                .All(option => option.IsCorrect);
        }
    }
}