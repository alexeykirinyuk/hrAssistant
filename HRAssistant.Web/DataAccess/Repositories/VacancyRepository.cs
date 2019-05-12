using System;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.DataAccess.Repositories
{
    public sealed class VacancyRepository : IVacancyRepository
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
    }
}
