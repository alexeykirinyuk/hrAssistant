using HRAssistant.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HRAssistant.DataAccess.Core
{
    internal interface IJobPositionRepository
    {
        Task<bool> Exists(Guid jobPositionId);

        void Add(JobPositionEntity entity);

        Task<JobPositionEntity> Get(Guid jobPositionId);

        IQueryable<JobPositionEntity> Search();
    }
}
