using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.JobPositionContracts;
using HRAssistant.Web.Admin.Contracts.Shared;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class GetJobPositionHandler : IQueryHandler<GetJobPosition, GetJobPositionResult>
    {
        private readonly IJobPositionRepository _jobPositionRepository;

        public GetJobPositionHandler(IJobPositionRepository jobPositionRepository)
        {
            Guard.AgainstNullArgument(nameof(jobPositionRepository), jobPositionRepository);

            _jobPositionRepository = jobPositionRepository;
        }

        public async Task<GetJobPositionResult> Handle(GetJobPosition query)
        {
            var entity = await _jobPositionRepository.Get(query.JobPositionId);

            return new GetJobPositionResult
            {
                JobPosition = new JobPosition
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Template = new Template
                    {
                        Description = entity.Template.Description,
                        Questions = entity.Template.Questions.Select(q => CreateContractQuestion(q)).ToArray()
                    }
                }
            };
        }

        private static Question CreateContractQuestion(QuestionEntity entity)
        {
            Question question;
            switch (entity)
            {
                case InputQuestionEntity inputEntity:
                    question = new InputQuestion
                    {
                        CorrectAnswer = inputEntity.CorrectAnswer
                    };
                    break;
                case SelectQuestionEntity select:
                    question = new SelectQuestion
                    {
                        OneCorrectAnswer = select.OneCorrectAnswer,
                        Options = select.Options.Select(o => new Option
                        {
                            IsCorrect = o.IsCorrect,
                            Title = o.Title
                        }).ToArray()
                    };
                    break;
                case GeneralQuestionEntity _:
                    question = new GeneralQuestion();
                    break;
                default:
                    throw new InvalidOperationException($"Can't create contract model from '{entity.GetType().Name}' entity.");
            }

            question.OrderIndex = entity.OrderIndex;
            question.Title = entity.Title;
            question.Description = entity.Description;

            return question;
        }
    }
}
