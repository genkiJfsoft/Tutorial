using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace ExpenseTracker.Extensions.Linq.Expressions;

public class ExtendableQueryProvider(IQueryProvider underlyingQueryProvider) : IAsyncQueryProvider
{
    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new ExpandableQuery<TElement>(this, expression);
    }

    public IQueryable CreateQuery(Expression expression)
    {
        Type elementType = expression.Type.GetElementType()!;
        try
        {
            return ((IQueryable)Activator.CreateInstance(typeof(ExpandableQuery<>).MakeGenericType(elementType), new object[] { this, expression })!)!;
        }
        catch (System.Reflection.TargetInvocationException tie)
        {
            throw tie.InnerException!;
        }
    }

    internal IEnumerable<T> ExecuteQuery<T>(Expression expression)
    {
        return underlyingQueryProvider.CreateQuery<T>(Visit(expression)).AsEnumerable();
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return underlyingQueryProvider.Execute<TResult>(Visit(expression));
    }

    public object Execute(Expression expression)
    {
        return underlyingQueryProvider.Execute(Visit(expression))!;
    }

    public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        return ((IAsyncQueryProvider)underlyingQueryProvider).ExecuteAsync<IAsyncEnumerable<TResult>>(Visit(expression),
            cancellationToken);
    }

    TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        return ((IAsyncQueryProvider)underlyingQueryProvider).ExecuteAsync<TResult>(Visit(expression), cancellationToken);
    }

    private Expression Visit(Expression exp)
    {
        ExpandableVisitor visitor = new(underlyingQueryProvider);
        Expression visitedExp = visitor.Visit(exp);

        return visitedExp;
    }
}
