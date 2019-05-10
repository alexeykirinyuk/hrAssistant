using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.DataAccess.Core
{
    internal interface ICityRepository
    {
        Task<CityEntity> Get(Guid cityId);

        IQueryable<CityEntity> Search();

        void Add(CityEntity city);
    }
}
