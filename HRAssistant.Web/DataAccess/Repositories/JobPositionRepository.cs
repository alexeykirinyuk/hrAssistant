using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;
using Microsoft.EntityFrameworkCore;

namespace HRAssistant.Web.DataAccess.Repositories
{
    internal sealed class JobPositionRepository : IJobPositionRepository
    {
        private readonly DatabaseContext _context;

        public JobPositionRepository(DatabaseContext context)
        {
            Guard.AgainstNullArgument(nameof(context), context);

            _context = context;
        }

        public void Add(JobPositionEntity entity)
        {
            Guard.AgainstNullArgument(nameof(entity), entity);

            _context.JobPositions.Add(entity);
        }

        public async Task<bool> Exists(Guid jobPositionId)
        {
            return await _context.JobPositions.AnyAsync(p => p.Id == jobPositionId);
        }

        public async Task<JobPositionEntity> Get(Guid jobPositionId)
        {
            var entity = await _context.JobPositions
                .Include(p => p.Template)
                .Include(p => p.Template.Questions)
                .Include("Template.Questions.Options")
                .SingleOrDefaultAsync(j => j.Id == jobPositionId);
            if (entity == null)
            {
                throw new InvalidOperationException($"Job Position with Id '{jobPositionId}' was not found.");
            }

            return entity;
        }

        public IQueryable<JobPositionEntity> Search() => _context.JobPositions;
    }
}
