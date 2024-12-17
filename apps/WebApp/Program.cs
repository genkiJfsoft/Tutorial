using ExpenseTracker.WebApp;
using ExpenseTracker.WebApp.Components;
using Serilog;

try
{
    Log.Logger = Builder.CreateBootstrapLogger();
    Log.Information("Starting up...");
    
    var app = WebApplication
        .CreateBuilder(args)
        .ConfigureBuilder()
        .Build();
    
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
