using CQRS.Core.Queries;

namespace CQRS.Core.Infrastructure
{
    public interface IQueryDispatcher<TEntity>
    {
        voidRegisterHandler<TQuery>(Func<TQuery, Task<List<TEntity>>> handler) where TQuery : BaseQuery;
    }
}