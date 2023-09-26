using LibraryAPI.Models;

namespace LibraryAPI.Services.Extensions
{
    public static class ListExtensions
    {
        public static List<TEntity> WithPagination<TEntity>(this IEnumerable<TEntity> entities, LibraryQuery query) where TEntity : class
        => entities.Skip(query.PageSize * (query.PageNumber - 1)).Take(query.PageSize).ToList();
    }
}
