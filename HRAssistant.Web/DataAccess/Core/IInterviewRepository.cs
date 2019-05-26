using HRAssistant.Web.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HRAssistant.Web.DataAccess.Core
{
    internal interface IInterviewRepository
    {
        void Add(InterviewEntity interview);

        Task<bool> Exists(Guid interviewId);

        Task<InterviewEntity> Get(Guid interviewId);

        Task<bool> HasOpenQuestion(Guid interviewId);

        IQueryable<InterviewEntity> Search();
    }
}
