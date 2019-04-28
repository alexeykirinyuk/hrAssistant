namespace HRAssistant.Infrastructure
{
    public abstract class SearchResult<TSearchItem>
    {
        public TSearchItem[] Items { get; set; }

        public PageOptions PageOptions { get; set; }
    }
}
