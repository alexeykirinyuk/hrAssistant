using System.Threading.Tasks;

namespace HRAssistant.Infrastructure.CQRS
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}
