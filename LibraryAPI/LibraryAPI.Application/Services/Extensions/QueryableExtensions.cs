using System.Linq.Expressions;

namespace LibraryAPI.Application.Services.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query, bool term, Expression<Func<TEntity, bool>> expression)
            => term ? query.Where(expression) : query;
    }
}
