using System.Collections;
using System.Linq.Expressions;

namespace ExpenseTracker.Extensions.Linq.Expressions;

public class ExpandableQuery<T>(ExtendableQueryProvider provider, Expression expression)
    : IOrderedQueryable<T>, IAsyncEnumerable<T>
{
    public Expression Expression { get; } = expression;
    public IQueryProvider Provider => provider;
    public Type ElementType  => typeof(T);

    public IEnumerator<T> GetEnumerator() => provider.ExecuteQuery<T>(Expression).GetEnumerator();

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return provider.ExecuteAsync<T>(Expression, cancellationToken).GetAsyncEnumerator(cancellationToken);
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
