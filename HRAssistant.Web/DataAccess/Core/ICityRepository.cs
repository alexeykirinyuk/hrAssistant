using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.DataAccess.Core
{
    internal interface ICityRepository
    {
        Task<bool> Exists(Guid id);

        Task<bool> Exists(string name, Guid? excludeCityId = null);

        Task<CityEntity> Get(Guid cityId);

        IQueryable<CityEntity> Search();

        void Add(CityEntity city);
    }
}
