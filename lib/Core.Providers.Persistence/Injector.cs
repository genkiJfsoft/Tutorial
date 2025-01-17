using ExpenseTracker.Core.Application.Services;
using ExpenseTracker.Core.Providers.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExpenseTracker.Core.Providers.Persistence;

public static class Injector
{
    ///<summary>
    /// Configures and adds the persistence services to the service collections.
    /// </summary>
    /// <returns>The service collection with the added persistence services.</returns>
    public static IServiceCollection AddCorePersistence(this IServiceCollection services, string? defaultConnectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(defaultConnectionString);
        
        services.AddScoped<ISaveChangesInterceptor, TimestampableEntityInterceptor>();
        
        services.AddDbContextFactory<DefaultDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            
            options.UseSqlServer(defaultConnectionString);
#if DEBUG
            options.LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
#endif
        }, ServiceLifetime.Scoped);

        services.AddScoped<IDbContext>(sp => sp.GetRequiredService<IDbContextFactory<DefaultDbContext>>().CreateDbContext());

        return services;
    }
    
    public static IdentityBuilder AddPersistenceStores(this IdentityBuilder builder)
    {
        builder.AddEntityFrameworkStores<DefaultDbContext>();
        return builder;
    }

}
