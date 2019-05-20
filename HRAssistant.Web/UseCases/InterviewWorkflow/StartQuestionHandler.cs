using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.DataAccess.Repositories;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;
using Microsoft.EntityFrameworkCore.Internal;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class StartQuestionHandler : ICommandHandler<StartQuestion, StartQuestionResult>
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StartQuestionHandler(IInterviewRepository interviewRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(interviewRepository), interviewRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _interviewRepository = interviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StartQuestionResult> Handle(StartQuestion command)
        {
            var interview = await _interviewRepository.Get(command.InterviewId.Value);
            var questions = interview.GetQuestions().ToArray();
            var currentQuestion = questions.GetCurrent();

            if (currentQuestion.Status == QuestionSagaStatusEntity.NotStarted)
            {
                currentQuestion.Status = QuestionSagaStatusEntity.Started;
                currentQuestion.StartUtcTime = DateTime.UtcNow;
            }

            await _unitOfWork.SaveChangesAsync();

            return new StartQuestionResult
            {
                Question = Convert(currentQuestion.Question),
                Index = questions.IndexOf(currentQuestion) + 1,
                TotalCount = questions.Length
            };
        }

        private static Question Convert(QuestionEntity entity)
        {
            Question question;
            switch (entity)
            {
                case GeneralQuestionEntity generalQuestionEntity:
                    question = new GeneralQuestion();
                    break;
                case InputQuestionEntity inputQuestionEntity:
                    question = new InputQuestion();
                    break;
                case SelectQuestionEntity selectQuestionEntity:
                    question = new SelectQuestion
                    {
                        OneCorrectAnswer = selectQuestionEntity.OneCorrectAnswer,
                        Options = selectQuestionEntity.Options
                            .Select(option => new Option
                            {
                                Id = option.Id,
                                Title = option.Title
                            })
                            .ToArray()
                    };
                    break;
                default:
                    throw new InvalidOperationException($"Can't cast '{entity.GetType().FullName}'.");
            }

            question.Title = entity.Title;
            question.Description = entity.Description;
            question.OrderIndex = entity.OrderIndex;

            return question;
        }
    }
}