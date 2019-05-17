using System;
using System.Threading.Tasks;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.DataAccess.Core
{
    internal interface IInterviewRepository
    {
        void Add(InterviewEntity interview);

        Task<bool> Exists(Guid id);

        Task<InterviewEntity> Get(Guid interviewId);

        Task<bool> HasNotCompletedQuestions(Guid interviewId);

        Task<QuestionSagaEntity> GetCurrentQuestion(Guid interviewId);
    }
}
