using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Infrastructure
{
    public abstract class SearchRequest<TResult> : IQuery<TResult>
    {
        public int? PageIndex { get; set; }
        
        public int? OnePageItemsCount { get; set; }
    }
}
