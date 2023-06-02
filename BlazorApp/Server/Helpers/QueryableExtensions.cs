using BlazorApp.Shared;

namespace BlazorApp.Server.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.ItemsPerPage)
                .Take(pagination.ItemsPerPage);
        }
    }
}
