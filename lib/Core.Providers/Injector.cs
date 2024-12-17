using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Core.Providers;

public static class Injector
{
    ///<summary>
    /// Configures and adds the providers services to the service collections.
    /// </summary>
    /// <returns>The service collection with the added providers services.</returns>
    public static IServiceCollection AddCoreProviders(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);
        
        return services;
    }
}
