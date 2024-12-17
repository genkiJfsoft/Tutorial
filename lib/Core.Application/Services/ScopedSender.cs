using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Core.Application.Services;

/// <inheritdoc cref="IScopedSender"/>
internal class ScopedSender(IServiceScopeFactory scopeFactory) : IScopedSender
{
    /// <inheritdoc/>
    public async IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var items = mediator.CreateStream(request, cancellationToken);

        await foreach (var item in items)
        {
            yield return item;
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<object?> CreateStream(object request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var items = mediator.CreateStream(request, cancellationToken);

        await foreach (var item in items)
        {
            yield return item;
        }
    }

    /// <inheritdoc/>
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var response = await mediator.Send(request, cancellationToken);

        return response;
    }

    /// <inheritdoc/>
    public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(request, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<object?> Send(object request, CancellationToken cancellationToken = default)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var response = await mediator.Send(request, cancellationToken);

        return response;
    }
}
