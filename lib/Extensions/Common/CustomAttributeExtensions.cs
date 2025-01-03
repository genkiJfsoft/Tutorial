using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ExpenseTracker.Extensions.Common;

public static class CustomAttributeExtensions
{
    public static DisplayAttribute? GetDisplayAttribute(this MemberInfo element) => element.GetCustomAttribute<DisplayAttribute>();
}
