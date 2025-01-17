using ExpenseTracker.Core.Domain;
using ExpenseTracker.Core.Domain.Authorization;
using ExpenseTracker.Core.Providers.Persistence;
using ExpenseTracker.Core.Providers.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Core.Providers;

public static class Injector
{
    ///<summary>
    /// Configures and adds the providers services to the service collections.
    /// </summary>
    /// <returns>The service collection with the added providers services.</returns>
    public static IServiceCollection AddCoreProviders(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddCorePersistence(configuration.GetConnectionString("DefaultConnection"));
        services.AddCoreIdentity();
        services.AddCoreAuthorization();

        return services;
    }
    
    private static IServiceCollection AddCoreIdentity(this IServiceCollection services)
    {
        services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(o => { });

        services.AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddPersistenceStores()
            .AddSignInManager()
            .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
            .AddDefaultTokenProviders();
        
        return services;
    }

    private static IServiceCollection AddCoreAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            // Add all Permissions as Policy which requires that the current user has the "Permission" claim
            // and the claim value match the specified permission value.
            foreach (var value in Permissions.GetValues())
            {
                options.AddPolicy(value, policy => policy.RequireClaim(CustomClaimTypes.Permission, value));
            }
        });

        return services;
    }


}
