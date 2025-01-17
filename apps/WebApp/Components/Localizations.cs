using System.Globalization;

namespace ExpenseTracker.WebApp.Components;

/// <summary>
/// Localization options constants
/// </summary>
public static class Localizations
{
    //public static readonly CultureInfo DefaultCulture = new CultureInfo("English");
    //public static readonly IReadOnlyList<CultureInfo> SupportedCultures = new List<CultureInfo>
    //{
    //    new CultureInfo("English"),
    //    new CultureInfo("Chinese")
    //};


    public static List<CultureInfo> SupportedCultures => [new("en"), new("zh")];

    public static CultureInfo DefaultCulture => SupportedCultures[0];
}
