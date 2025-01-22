using System.Globalization;
using System.Resources;

namespace ExpenseTracker.WebApp.Resources;

public class Strings
{
    private static readonly ResourceManager _resourceManager =
          new ResourceManager("ExpenseTracker.WebApp.Resources.Strings", typeof(Strings).Assembly);

    public static string EmailRequired =>
        _resourceManager.GetString("EmailRequired", CultureInfo.CurrentUICulture)
        ?? "Default email required message";

    public static string PasswordRequired =>
        _resourceManager.GetString("PasswordRequired", CultureInfo.CurrentUICulture)
        ?? "Default password required message";

    public static string InvalidEmail =>
        _resourceManager.GetString("InvalidEmail", CultureInfo.CurrentUICulture)
        ?? "Default invalid email message";

    public static string FullNameRequired =>
    _resourceManager.GetString("FullNameRequired", CultureInfo.CurrentUICulture)
    ?? "Default invalid email message";

    public static string MobileNoRequired =>
  _resourceManager.GetString("MobileNoRequired", CultureInfo.CurrentUICulture)
  ?? "Default invalid email message";

    public static string InvalidMobileNo =>
    _resourceManager.GetString("InvalidMobileNo", CultureInfo.CurrentUICulture)
    ?? "Default invalid email message";

    public static string UsernameRequired   =>
_resourceManager.GetString("UsernameRequired", CultureInfo.CurrentUICulture)
?? "Default invalid email message";

    public static string VerifyPassword =>
_resourceManager.GetString("VerifyPassword", CultureInfo.CurrentUICulture)
?? "Default invalid email message";

    public static string PasswordsNotMatch =>
_resourceManager.GetString("PasswordsNotMatch", CultureInfo.CurrentUICulture)
?? "Default invalid email message";


}
