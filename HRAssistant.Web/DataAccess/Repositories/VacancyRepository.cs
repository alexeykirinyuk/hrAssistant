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
        private readonly DatabaseContext _databaseContext;

        public VacancyRepository(DatabaseContext databaseContext)
        {
            Guard.AgainstNullArgument(nameof(databaseContext), databaseContext);

            _databaseContext = databaseContext;
        }

        public void Add(VacancyEntity vacancy)
        {
            Guard.AgainstNullArgument(nameof(vacancy), vacancy);

            _databaseContext.Vacancies.Add(vacancy);
        }

        public async Task<bool> Exists(Guid vacancyId)
        {
            return await _databaseContext.Vacancies.AnyAsync(v => v.Id == vacancyId);
        }

        public Task<VacancyEntity> Get(Guid vacancyId)
        {
            var entity = _databaseContext.Vacancies.SingleOrDefaultAsync(v => v.Id == vacancyId);

            if (entity == null)
            {
                throw new InvalidOperationException($"Vacancy with id '{vacancyId}' was not found.");
            }

            return entity;
        }

        public async Task<VacancyStatusEntity> GetStatus(Guid vacancyId)
        {
            var vacancyStatus = await _databaseContext.Vacancies
                .Where(v => v.Id == vacancyId)
                .Select(v => (VacancyStatusEntity?)v.Status)
                .SingleOrDefaultAsync();

            if (!vacancyStatus.HasValue)
            {
                throw new InvalidOperationException($"Vacancy with id '{vacancyId}' was not found.");
            }

            return vacancyStatus.Value;
        }

        public IQueryable<VacancyEntity> Search() => _databaseContext.Vacancies;
    }
}