namespace ExpenseTracker.Extensions.Linq.Expressions;

public class ReplaceWithExpressionAttribute: Attribute
{
    /// <summary>
    /// the name of the method returning an expression which you want to replace this extension with.
    /// </summary>
    public string? MethodName { get; set; }

    /// <summary>
    /// the name of the property returning an expression which you want to replace this extension with.
    /// </summary>
    public string? PropertyName { get; set; }
}
