using System.Threading.Tasks;
using HRAssistant.DataAccess.Core;
using LiteGuard;

namespace HRAssistant.DataAccess.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            Guard.AgainstNullArgument(nameof(context), context);

            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
