using System.Linq.Expressions;

namespace LibraryAPI.Services.Extensions
{
    public static class QueryableExtensions
    {
        //Execute where predicate when term is true, else return query
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query, bool term, Expression<Func<TEntity, bool>> expression)
            => term ? query.Where(expression) : query;
    }
}
