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

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Interviews.AnyAsync(interview => interview.CandidateId == id);
        }

        public async Task<InterviewEntity> Get(Guid id)
        {
            var interview = await _context.Interviews.SingleOrDefaultAsync(i => i.Id == id);
            if (interview == null)
            {
                throw new InvalidOperationException($"Interview with id '{id}' was not found.");
            }

            return interview;
        }
    }
}
