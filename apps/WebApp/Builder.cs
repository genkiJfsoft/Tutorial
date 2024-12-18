using ExpenseTracker.Core.Application;
using ExpenseTracker.Core.Providers;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;

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
        
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
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
