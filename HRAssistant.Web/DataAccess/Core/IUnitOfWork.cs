using System;
using System.Threading.Tasks;

namespace HRAssistant.DataAccess.Core
{
    internal interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
