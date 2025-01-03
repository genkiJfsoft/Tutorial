using System.Linq.Expressions;
using System.Reflection;

namespace ExpenseTracker.Extensions.Linq.Expressions;

public class ExpandableVisitor: ExpressionVisitor
	{
		private readonly IQueryProvider _provider;
		private readonly Dictionary<ParameterExpression, Expression> _replacements = new Dictionary<ParameterExpression, Expression>();

		internal ExpandableVisitor(IQueryProvider provider)
		{
			_provider = provider;
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			bool expandNode = node.Method.GetCustomAttributes(typeof(ExpandQueryableAttribute), false).Length != 0;
			if (expandNode && node.Method.IsStatic)
			{
				object?[] args = new object?[node.Arguments.Count];
				args[0] = _provider.CreateQuery(node.Arguments[0]);

				for (int i = 1; i < node.Arguments.Count; i++)
				{
					Expression arg = node.Arguments[i];
					args[i] = (arg.NodeType == ExpressionType.Constant) ? ((ConstantExpression)arg).Value : arg;
				}
				return Visit((((IQueryable)node.Method.Invoke(null, args)!)!).Expression);
			}
			var replaceNodeAttribute = node.Method.GetCustomAttributes(typeof(ReplaceWithExpressionAttribute), false).Cast<ReplaceWithExpressionAttribute>().FirstOrDefault();
			if (replaceNodeAttribute != null && node.Method.IsStatic)
			{
			    if (!string.IsNullOrEmpty(replaceNodeAttribute.MethodName))
			    {
			        var methods = node.Method.DeclaringType!.GetRuntimeMethods();
			        var replaceWith = methods.First(x => x.Name == replaceNodeAttribute.MethodName).Invoke(null, null);
			        if (replaceWith is LambdaExpression expression)
			        {
			            RegisterReplacementParameters(node.Arguments.ToArray(), expression);
			            return Visit(expression.Body);
			        }
			    }
			    if (!string.IsNullOrEmpty(replaceNodeAttribute.PropertyName))
			    {
			        var properties = node.Method.DeclaringType!.GetRuntimeProperties();
			        var replaceWith = properties.First(x => x.Name == replaceNodeAttribute.PropertyName).GetValue(null);
			        if (replaceWith is LambdaExpression expression)
			        {
			            RegisterReplacementParameters(node.Arguments.ToArray(), expression);
			            return Visit(expression?.Body)!;
			        }
                }
            }
			return base.VisitMethodCall(node);
		}
		protected override Expression VisitParameter(ParameterExpression node)
		{
			Expression replacement;
			return _replacements.TryGetValue(node, out replacement!) ? Visit(replacement) : base.VisitParameter(node);
        }
		private void RegisterReplacementParameters(Expression[] parameterValues, LambdaExpression expressionToVisit)
		{
			if (parameterValues.Length != expressionToVisit.Parameters.Count)
				throw new ArgumentException(
                    $"The parameter values count ({parameterValues.Length}) does not match the expression parameter count ({expressionToVisit.Parameters.Count})");
			foreach (var x in expressionToVisit.Parameters.Select((p, idx) => new { Index = idx, Parameter = p }))
			{
				if (_replacements.ContainsKey(x.Parameter))
				{
					throw new Exception("Parameter already registered, this shouldn't happen.");
				}
				_replacements.Add(x.Parameter, parameterValues[x.Index]);
			}
		}
	}
