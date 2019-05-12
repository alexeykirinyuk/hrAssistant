using System;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Domain;

namespace HRAssistant.Web.DataAccess.Core
{
    internal interface IUserRepository
    {
        Task<bool> Exists(Guid userId);

        Task<bool> ExistsByUsername(string username, Guid? excludeUserId = null);

        Task<UserEntity> Get(Guid userId);

        void Add(UserEntity user);

        IQueryable<UserEntity> Search();
    }
}
