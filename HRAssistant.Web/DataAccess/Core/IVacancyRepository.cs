using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.DataAccess.Core
{
    internal interface IVacancyRepository
    {
        void Add(VacancyEntity vacancy);

        Task<bool> Exists(Guid vacancyId);

        Task<VacancyEntity> Get(Guid vacancyId);

        Task<VacancyStatusEntity> GetStatus(Guid vacancyId);

        IQueryable<VacancyEntity> Search();
    }
}
