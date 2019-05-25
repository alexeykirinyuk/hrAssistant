using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;
using Microsoft.EntityFrameworkCore;

namespace HRAssistant.Web.DataAccess.Repositories
{
    internal sealed class VacancyRepository : IVacancyRepository
    {
        private readonly DatabaseContext _context;

        public VacancyRepository(DatabaseContext context)
        {
            Guard.AgainstNullArgument(nameof(context), context);

            _context = context;
        }

        public void Add(VacancyEntity vacancy)
        {
            Guard.AgainstNullArgument(nameof(vacancy), vacancy);

            _context.Vacancies.Add(vacancy);
        }

        public async Task<bool> Exists(Guid vacancyId)
        {
            return await _context.Vacancies.AnyAsync(v => v.Id == vacancyId);
        }

        public async Task<VacancyEntity> Get(Guid vacancyId)
        {
            var entity = await _context.Vacancies
                .Include(v => v.JobPosition)
                .Include(v => v.Form)
                .Include(v => v.Form.Questions)
                .Include("Form.Questions.Options")
                .SingleOrDefaultAsync(v => v.Id == vacancyId);

            if (entity == null)
            {
                throw new InvalidOperationException($"Vacancy with id '{vacancyId}' was not found.");
            }

            return entity;
        }

        public async Task<VacancyStatusEntity> GetStatus(Guid vacancyId)
        {
            var vacancyStatus = await _context.Vacancies
                .Where(v => v.Id == vacancyId)
                .Select(v => (VacancyStatusEntity?)v.Status)
                .SingleOrDefaultAsync();

            if (!vacancyStatus.HasValue)
            {
                throw new InvalidOperationException($"Vacancy with id '{vacancyId}' was not found.");
            }

            return vacancyStatus.Value;
        }

        public IQueryable<VacancyEntity> Search() => _context.Vacancies;
    }
}