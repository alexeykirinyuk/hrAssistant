using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.DataAccess.Core
{
    internal interface IJobPositionRepository
    {
        Task<bool> Exists(Guid jobPositionId);

        void Add(JobPositionEntity entity);

        Task<JobPositionEntity> Get(Guid jobPositionId);

        IQueryable<JobPositionEntity> Search();
    }
}
