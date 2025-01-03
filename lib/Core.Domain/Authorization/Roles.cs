using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ExpenseTracker.Core.Domain.Authorization;

/// <summary>
/// Defines the roles available in the application.
/// </summary>
public static class Roles
{
    public static class User
    {
        [Display(Name="Administrator")]
        public const string Admin = "User.Admin";
        
        [Display(Name="Manager")]
        public const string Manager = "User.Manager";
        
        [Display(Name="Basic User")]
        public const string Basic = "User.Basic";
    }

    public static List<FieldInfo> GetAll()
    {
        return typeof(Roles)
            .GetNestedTypes()
            .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static |  BindingFlags.FlattenHierarchy))
            .ToList();
    }

    /// <summary>
    /// Returns all role names defined in this constants class.
    /// </summary>
    /// <remarks>
    /// This method uses reflection to retrieve the names of all public static fields nested in the <see cref="Roles"/> class.
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

