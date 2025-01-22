using ExpenseTracker.Core.Providers.Persistence.Services;
using ExpenseTracker.Core.Application;
using ExpenseTracker.Core.Application.Services;
using ExpenseTracker.Core.Providers;
using ExpenseTracker.Core.Providers.Persistence;
using ExpenseTracker.WebApp.Endpoints;
using ExpenseTracker.WebApp.Infrastructure;
using ExpenseTracker.WebApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using ExpenseTracker.WebApp.Components;
using ExpenseTracker.Extensions.Authorization;
using Microsoft.AspNetCore.Identity;
using ExpenseTracker.Core.Domain;
namespace ExpenseTracker.WebApp;

/// <summary>
/// Provides extension methods to configure the <see cref="WebApplicationBuilder"/>.
/// </summary>
public static class Builder
{
    /// <summary>
    /// Configure the <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <returns>A configured <see cref="WebApplicationBuilder"/>.</returns>
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddSerilog((s, c) => c
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(s));

        builder.Services.AddCoreProviders(builder.Configuration);
        builder.Services.AddCoreApplication();
      
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = Pages.Auth.LoginPage.Route;
            options.LogoutPath = AuthEndpoints.LogoutRoute;
            options.AccessDeniedPath = Pages.Auth.AccessDeniedPage.Route;
        });
        
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IUserContext, HttpUserContext>();
        
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddBlazorBootstrap();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddHttpContextAccessor();

        //builder.Services.AddScoped<ICookieService, CookieService>();



        builder.Services.AddAuthorization(options =>

        {
            options.AddPolicy("MerchantOnly", policy =>
                policy.RequireClaim("Category", "Merchant")); // Check if Category is "Merchant"


        });

        builder.Services.AddLocalization();
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SetDefaultCulture(Localizations.DefaultCulture.Name);
            options.SupportedCultures = Localizations.SupportedCultures;
            options.SupportedUICultures = Localizations.SupportedCultures;
            options.FallBackToParentUICultures = true;
        });

#if DEBUG
        builder.Services.AddSassCompiler();
#endif
        
        return builder;
    }

    /// <summary>
    /// Creates a temporary logger instance that can be used to log application startup
    /// events until the full logger configuration is available.
    /// </summary>
    public static ReloadableLogger CreateBootstrapLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();
    }
}
