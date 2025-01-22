using System.Globalization;
using ExpenseTracker.Core.Providers.Persistence;
using ExpenseTracker.WebApp;
using ExpenseTracker.WebApp.Endpoints;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Serilog;

try
{

    Log.Logger = Builder.CreateBootstrapLogger();
    Log.Information("Starting up...");
    
    var app = WebApplication
        .CreateBuilder(args)
        .ConfigureBuilder()
        .Build();

    var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
 
    app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

    app.Use(async (context, next) =>
    {
        Console.WriteLine($"Current UI Culture: {CultureInfo.CurrentUICulture.Name}");
        await next.Invoke();
    });
    #region Initialize the database
    using var scope = app.Services.CreateScope();
    await DbInitializer.InitializeDatabaseAsync(scope.ServiceProvider);
    await DbInitializer.Identity.EnsureRolesAsync(scope.ServiceProvider);
    await DbInitializer.Identity.EnsureDefaultUserAsync(scope.ServiceProvider);
    await DbInitializer.Data.SeedAsync(scope.ServiceProvider);
    if (app.Environment.IsDevelopment()) await DbInitializer.DummyData.SeedAsync(scope.ServiceProvider);
    #endregion
    
    Log.Information("Configuring host...");

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();
    app.UseAntiforgery();

    app.MapStaticAssets();
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();
    
    app.MapAuthEndpoints();
    app.MapLocalizationsEndpoints();
    
    Log.Information("Running...");
    
    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
}
finally
{
    Log.CloseAndFlush();
}
