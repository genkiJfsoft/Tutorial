using System.Reflection;

namespace ExpenseTracker.Core.Domain.Authorization;

public static partial class Permissions
{
    public static class Expenses
    {
        public const string Manage = "Expenses.Manage";
    }
    
    /// <summary>
    /// Returns all permission names defined in this constants class.
    /// </summary>
    /// <remarks>
    /// This method uses reflection to retrieve the names of all public static fields nested in the <see cref="Permissions"/> class.
    /// </remarks>
    /// <returns>An array of strings containing the permission names.</returns>
    public static List<string> GetValues()
    {
        return typeof(Permissions)
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
