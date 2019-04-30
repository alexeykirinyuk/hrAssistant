using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Infrastructure
{
    public abstract class SearchRequest<TResult> : IQuery<TResult>
    {
        public int? PageIndex { get; set; }
        
        public int? OnePageItemsCount { get; set; }
    }
}
