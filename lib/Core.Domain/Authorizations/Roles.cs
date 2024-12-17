using System.Reflection;

namespace ExpenseTracker.Core.Domain.Authorizations;

/// <summary>
/// Defines the roles available in the application.
/// </summary>
public static class Roles
{
    public static class User
    {
        public const string Admin = "User.Admin";
        public const string Manager = "User.Manager";
        public const string Basic = "User.Basic";
    }

    /// <summary>
    /// Returns all role names defined in this constants class.
    /// </summary>
    /// <remarks>
    /// This method uses reflection to retrieve the names of all public static fields in the Roles class and its nested types.
    /// </remarks>
    /// <returns>An array of strings containing the role names.</returns>
    public static List<string> GetValues()
    {
        return typeof(Roles)
            .GetNestedTypes()
            .SelectMany(t => t
                .GetFields(BindingFlags.Public |
                   BindingFlags.Static |
                   BindingFlags.FlattenHierarchy
                )
                .Select(f => f.GetValue(null)?.ToString())
                .Where(e => !string.IsNullOrEmpty(e))
            )
            .OfType<string>()
            .ToList();
    }
}

