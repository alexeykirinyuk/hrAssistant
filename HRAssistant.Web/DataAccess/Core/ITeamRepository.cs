using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.DataAccess.Core
{
    internal interface ITeamRepository
    {
        Task<bool> Exists(Guid teamId);

        Task<bool> Exists(string teamTitle, Guid? excludeTeamId = null);

        Task<TeamEntity> Get(Guid teamId);

        void Add(TeamEntity team);

        IQueryable<TeamEntity> Search();
    }
}
