//using Microsoft.AspNetCore.Components;
//using ExpenseTracker.Core.Providers.Persistence.Services;
//using System.Globalization;
//using System.Resources;
//using ExpenseTracker.WebApp.Resources;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//namespace ExpenseTracker.WebApp.Infrastructure;

//public abstract class BaseLocalizedComponent : ComponentBase
//{
//    [Inject] protected UserService UserService { get; set; } = default!;
//    [Inject] protected LanguageService LanguageService { get; set; } = default!;

//    [Inject] protected NavigationManager Navigation { get; set; } = default!;
//    protected string currentCulture { get;  set; } = "en";

//    protected async Task ChangeLanguage(string culture)
//    {
//        // Update the culture globally
//        CultureInfo.CurrentCulture = new CultureInfo(culture);
//        CultureInfo.CurrentUICulture = new CultureInfo(culture);
//        Console.WriteLine($"Overriding ChangeLanguage to set language: {culture}");
//        LanguageService.SetLanguage(culture);
//        // Update the local variable for display
//        currentCulture = culture;

//        // Debug output
//        Console.WriteLine($"ChangeLanguage - Current Culture: {CultureInfo.CurrentCulture.Name}");
//        Console.WriteLine($"ChangeLanguage - Current UI Culture: {CultureInfo.CurrentUICulture.Name}");
//        Console.WriteLine($"ChangeLanguage - Preferred Language: {currentCulture}");

//        // Save to the database
//        var userId = await UserService.GetCurrentUserIdAsync();
//        if (userId != null)
//        {
//            var success = await UserService.UpdatePreferredLanguageAsync(userId, culture);
//            if (success)
//            {
//                Console.WriteLine($"Preferred language updated to: {culture}");

//            }
//            else
//            {
//                Console.WriteLine("Failed to update preferred language.");
//            }
//        }

//        // Trigger a UI refresh
//        Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
//    }
//   protected string L(string key)
//    {
//        try
//        {
//            ResourceManager rm = new ResourceManager("ExpenseTracker.WebApp.Resources.Strings", typeof(Strings).Assembly);
//            string localizedValue = rm.GetString(key, new CultureInfo(currentCulture)) ?? $"[Missing: {key}]";
//            Console.WriteLine($"Localized '{key}': {localizedValue}");
//            return localizedValue;
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error retrieving localized text for key '{key}': {ex.Message}");
//            return $"[Error: {key}]";
//        }
//    }

//    protected override async Task OnInitializedAsync()
//    {
//        var userId = await UserService.GetCurrentUserIdAsync();
//        if (userId != null)
//        {
//            var preferredLanguage = await UserService.GetPreferredLanguageAsync(userId);
//            if (!string.IsNullOrEmpty(preferredLanguage))
//            {
//                // Apply the saved culture
//                CultureInfo.CurrentCulture = new CultureInfo(preferredLanguage);
//                CultureInfo.CurrentUICulture = new CultureInfo(preferredLanguage);
//                currentCulture = preferredLanguage;
//            }
//        }
//        Console.WriteLine($"Applied culture on load: {currentCulture}");
//    }

//    protected override void OnInitialized()
//    {
//        LanguageService.OnChange += HandleLanguageChange;
//    }

//    private void HandleLanguageChange()
//    {
//        InvokeAsync(() =>
//        {
//            StateHasChanged(); // Notify the component to re-render on the correct thread
//        });
//    }


//}
