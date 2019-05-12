using System;
using System.Linq;
using HRAssistant.Web.Admin.Contracts.JobPositionContracts;
using HRAssistant.Web.Admin.Contracts.Shared;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    public static class JobPositionUpdateExtensions
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
    }
}
