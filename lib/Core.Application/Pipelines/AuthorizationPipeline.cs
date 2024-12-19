using System.Reflection;
using System.Security.Claims;
using ExpenseTracker.Core.Application.Exceptions;
using ExpenseTracker.Core.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Core.Application.Pipelines;

internal class AuthorizationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUserContext _userContext;
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationPipeline(
        IUserContext userContext,
        IAuthorizationService authorizationService)
    {
        _userContext = userContext;
        _authorizationService = authorizationService;
    }

    /// <summary>
    /// Handles the authorization for the given request by checking for role-based and policy-based authorizations.
    /// If the request requires authorization, it ensures the user represented by the claims principal is authenticated 
    /// and verifies that the user fulfills the required roles and policies. If authorization is not required or is successfully validated,
    /// the next delegate in the pipeline is invoked.
    /// </summary>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            var principal = _userContext.User?.Identity?.IsAuthenticated ?? false ? _userContext.User : throw new UnauthorizedAccessException();

            HandleRoleBasedAuthorizations(principal, authorizeAttributes);
            await HandlePolicyBasedAuthorizationsAsync(principal, authorizeAttributes);
        }

        // User is authorized / authorization not required
        return await next();
    }

    /// <summary>
    /// Handles role-based authorizations for the given request by checking if the user represented by the claims principal fulfills the required roles.
    /// Otherwise, a <see cref="ForbiddenAccessException"/> is thrown.
    /// </summary>
    private static void HandleRoleBasedAuthorizations(ClaimsPrincipal principal, IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

        if (authorizeAttributesWithRoles.Any())
        {
            var authorized = false;

            foreach (var roles in authorizeAttributesWithRoles.Where(a => a.Roles != null).Select(a => a.Roles!.Split(',')))
            {
                if (roles.Any(principal.IsInRole))
                {
                    authorized = true;
                }
            }

            // Must be a member of at least one role in roles
            if (!authorized)
            {
                throw new ForbiddenAccessException();
            }
        }
    }

    /// <summary>
    /// Handles policy-based authorizations for the given request by checking if the user represented by the claims principal fulfills 
    /// the required policies. Otherwise, a <see cref="ForbiddenAccessException"/> is thrown.
    /// </summary>
    private async Task HandlePolicyBasedAuthorizationsAsync(ClaimsPrincipal principal, IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
        if (authorizeAttributesWithPolicies.Any())
        {
            foreach (var policy in authorizeAttributesWithPolicies.Where(a => a.Policy != null).Select(a => a.Policy!))
            {
                var result = await _authorizationService.AuthorizeAsync(principal, policy);

                var authorized = result.Succeeded;

                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }
        }
    }
}
