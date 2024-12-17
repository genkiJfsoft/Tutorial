using System.Reflection;
using ExpenseTracker.Core.Application.Pipelines;
using ExpenseTracker.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Core.Application;

public static class Injector
{
    /// <summary>
    /// Configures and adds the application services to the service collections.
    /// </summary>
    /// <returns>The service collection with the added application services.</returns>
    public static IServiceCollection AddCoreApplication(this IServiceCollection services)
    {
        var assemblyToScan = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assemblyToScan);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionPipeline<,>));
        });

        services.AddScoped<IScopedSender, ScopedSender>();

        return services;
    }
}
