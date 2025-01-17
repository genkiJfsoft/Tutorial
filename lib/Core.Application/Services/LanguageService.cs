//using System.Globalization;
//using Microsoft.AspNetCore.Components;

//public class LanguageService
//{
//    public string CurrentLanguage { get; private set; } = "en";

//    // Event to notify components of a language change
//    public event Action? OnChange;

//    // Method to set the language and notify subscribers
//    public void SetLanguage(string culture)
//    {
//        // Avoid redundant operations if the same language is set
//        if (CurrentLanguage == culture) return;

//        // Update the current language
//        CurrentLanguage = culture;

//        // Set the global culture for the application
//        ApplyCulture(culture);

//        // Notify all subscribers about the language change
//        NotifyStateChanged();
//    }

//    // Apply the culture globally for threads
//    private void ApplyCulture(string culture)
//    {
//        var cultureInfo = new CultureInfo(culture);
//        CultureInfo.CurrentCulture = cultureInfo;
//        CultureInfo.CurrentUICulture = cultureInfo;
//        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
//        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
//    }

//    // Notify components or services subscribed to language changes
//    private void NotifyStateChanged() {
//        Console.WriteLine("NotifyStateChanged called.");

//        OnChange?.Invoke();
//    } 
//}
