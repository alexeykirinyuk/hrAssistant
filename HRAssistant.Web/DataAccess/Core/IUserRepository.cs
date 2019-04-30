using HRAssistant.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HRAssistant.DataAccess.Core
{
    internal interface IUserRepository
    {
        Task<bool> Exists(Guid userId);

        Task<bool> ExistsByUsername(string username);

        Task<UserEntity> Get(Guid userId);

        void Add(UserEntity user);

        IQueryable<UserEntity> Search();
    }
}
