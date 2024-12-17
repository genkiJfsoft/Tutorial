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
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
        return builder;
    }
}
