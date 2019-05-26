using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;
using Microsoft.EntityFrameworkCore;

namespace HRAssistant.Web.DataAccess.Repositories
{
    internal sealed class InterviewRepository : IInterviewRepository
    {
        private readonly DatabaseContext _context;

        public InterviewRepository(DatabaseContext context)
        {
            Guard.AgainstNullArgument(nameof(context), context);

            _context = context;
        }

        public void Add(InterviewEntity interview)
        {
            Guard.AgainstNullArgument(nameof(interview), interview);

            _context.Interviews.Add(interview);
        }

        public async Task<bool> Exists(Guid interviewId)
        {
            return await _context.Interviews.AnyAsync(interview => interview.Id == interviewId);
        }

        public async Task<InterviewEntity> Get(Guid interviewId)
        {
            var interview = await _context.Interviews
                .Include(i => i.Vacancy.Team.City)
                .Include(i => i.Vacancy.JobPosition)
                .Include(i => i.Candidate)
                .Include(i => i.Result)
                .Include(i => i.FormSagaEntity.Questions)
                .Include("FormSagaEntity.Questions.Question")
                .Include("FormSagaEntity.Questions.SelectedOptions")
                .SingleOrDefaultAsync(i => i.Id == interviewId);
            if (interview == null)
            {
                throw new InvalidOperationException($"Interview with id '{interviewId}' was not found.");
            }

            return interview;
        }

        public async Task<bool> HasOpenQuestion(Guid interviewId)
        {
            return await _context.Interviews
                .Where(interview => interview.Id == interviewId)
                .SelectMany(interview => interview.FormSagaEntity.Questions)
                .OrderBy(question => question.Question.OrderIndex)
                .AnyAsync(question => question.Status == QuestionSagaStatusEntity.Started ||
                                      question.Status == QuestionSagaStatusEntity.NotStarted);
        }

        public IQueryable<InterviewEntity> Search() => _context.Interviews;
    }
}