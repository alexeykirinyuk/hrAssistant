using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HRAssistant.Web.Infrastructure
{
    public static class FilterExtensions
    {
        public static IQueryable<TSource> FilterBy<TSource, TProperty>(
            this IQueryable<TSource> queryable,
            TProperty property,
            Expression<Func<TSource, bool>> filter)
        {
            if (property == null)
            {
                return queryable;
            }

            return queryable.Where(filter);
        }

        public static async Task<TSearchResult> ToSearchResults<TSearchResult, TItem>(
            this IQueryable<TItem> queryable,
            SearchRequest<TSearchResult> searchRequest)
            where TSearchResult : SearchResult<TItem>, new()
        {
            var pageIndex = searchRequest.PageIndex ?? 1;
            var pageCount = await queryable.CalculatePageCount(searchRequest.OnePageItemsCount);
            queryable = queryable.ApplyPaginationFiltering(pageIndex, searchRequest.OnePageItemsCount);

            var items = await queryable.ToArrayAsync();

            return new TSearchResult
            {
                Items = items,
                PageOptions = new PageOptions
                {
                    PageIndex = pageIndex,
                    PageCount = pageCount
                }
            };
        }

        private static IQueryable<TItem> ApplyPaginationFiltering<TItem>(
            this IQueryable<TItem> queryable,
            int pageIndex,
            int? onePageItemsCount)
        {
            if (onePageItemsCount.HasValue)
            {
                queryable = queryable
                    .Skip((pageIndex - 1) * onePageItemsCount.Value)
                    .Take(onePageItemsCount.Value);
            }

            return queryable;
        }

        private static async Task<int?> CalculatePageCount<TItem>(
            this IQueryable<TItem> items,
            int? onePageItemsCount)
        {
            if (!onePageItemsCount.HasValue)
            {
                return null;
            }

            var count = await items.CountAsync();

            return (int)Math.Ceiling((double)count / onePageItemsCount.Value);
        }
    }
}
