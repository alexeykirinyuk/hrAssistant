using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.DataAccess;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;
using Microsoft.EntityFrameworkCore;

namespace HRAssistant.Web.DataAccess.Repositories
{
    internal sealed class TeamRepository : ITeamRepository
    {
        private readonly DatabaseContext _context;

        public TeamRepository(DatabaseContext context)
        {
            Guard.AgainstNullArgument(nameof(context), context);

            _context = context;
        }

        public async Task<bool> Exists(Guid teamId)
        {
            return await _context.Teams.AnyAsync(t => t.Id == teamId);
        }

        public async Task<bool> Exists(string teamTitle, Guid? excludeTeamId = null)
        {
            Guard.AgainstNullArgument(nameof(teamTitle), teamTitle);

            return await _context.Teams
                .AnyAsync(t => t.Title == teamTitle && (!excludeTeamId.HasValue || t.Id != excludeTeamId.Value));
        }

        public async Task<TeamEntity> Get(Guid teamId)
        {
            var entity = await _context.Teams.FindAsync(teamId);
            if (entity == null)
            {
                throw new InvalidOperationException($"Team with id '{teamId}' was not found.");
            }

            return entity;
        }

        public void Add(TeamEntity team)
        {
            Guard.AgainstNullArgument(nameof(team), team);

            _context.Teams.Add(team);
        }

        public IQueryable<TeamEntity> Search()
        {
            return _context.Teams;
        }
    }
}