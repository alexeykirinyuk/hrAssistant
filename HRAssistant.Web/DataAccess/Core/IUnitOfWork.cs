using System.Threading.Tasks;

namespace HRAssistant.Web.DataAccess.Core
{
    internal interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
