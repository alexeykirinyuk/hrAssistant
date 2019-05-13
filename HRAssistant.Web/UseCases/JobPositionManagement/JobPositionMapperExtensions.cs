using System;
using System.Linq;
using HRAssistant.Web.Contracts.JobPositionManagement;
using HRAssistant.Web.Contracts.Shared;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.UseCases.JobPositionManagement
{
    public static class JobPositionMapperExtensions
    {
        public static void Update(this JobPositionEntity entity, JobPosition job)
        {
            Guard.AgainstNullArgument(nameof(entity), entity);
            Guard.AgainstNullArgument(nameof(job), job);

            entity.Title = job.Title;

            var templateEntity = entity.Template;
            templateEntity.Description = job.Template.Description;
            templateEntity.Questions.Clear();
            templateEntity.Questions.AddRange(job.Template.Questions.Select(q => CreateQuestionEntity(q)));
        }

        public static QuestionEntity CreateQuestionEntity(this Question q)
        {
            var questionEntity = CreateAndInitSpecifiedProperties(q);
            questionEntity.Title = q.Title;
            questionEntity.OrderIndex = q.OrderIndex.Value;
            questionEntity.Description = q.Description;

            return questionEntity;
        }

        private static QuestionEntity CreateAndInitSpecifiedProperties(Question question)
        {
            switch (question)
            {
                case InputQuestion input:
                    return new InputQuestionEntity
                    {
                        CorrectAnswer = input.CorrectAnswer
                    };
                case SelectQuestion select:
                    return new SelectQuestionEntity
                    {
                        Options = select.Options.Select(o => new OptionEntity
                        {
                            Title = o.Title,
                            IsCorrect = o.IsCorrect
                        }).ToList(),
                        OneCorrectAnswer = select.OneCorrectAnswer
                    };
                case GeneralQuestion _:
                    return new GeneralQuestionEntity();
                default:
                    throw new InvalidOperationException($"Can't create entity for '{question.GetType().Name}' question.");
            }
        }

        public static Question CreateContractQuestion(this QuestionEntity entity)
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
