using System;
using System.Collections.Generic;
using System.Linq;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    public static class InterviewExtensions
    {
        public static IEnumerable<QuestionSagaEntity> GetQuestions(this InterviewEntity interview)
        {
            Guard.AgainstNullArgument(nameof(interview), interview);

            return interview
                .FormSagaEntity
                .Questions
                .OrderBy(question => question.Question.OrderIndex);
        }

        public static QuestionSagaEntity GetCurrent(this IEnumerable<QuestionSagaEntity> questions)
        {
            Guard.AgainstNullArgument(nameof(questions), questions);

            var currentQuestion = questions.FirstOrDefault(IsOpen);

            if (currentQuestion == null)
            {
                throw new InvalidOperationException("Enumerable does not contain open questions.");
            }

            return currentQuestion;
        }

        public static bool IsOpen(this QuestionSagaEntity question)
        {
            Guard.AgainstNullArgument(nameof(question), question);

            return question.Status == QuestionSagaStatusEntity.NotStarted ||
                   question.Status == QuestionSagaStatusEntity.Started;
        }
    }
}