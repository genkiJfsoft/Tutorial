using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace ExpenseTracker.WebApp.Endpoints;

internal static class LocalizationEndpoints
{
    // Add endpoints to set current culture
    public static IEndpointConventionBuilder MapLocalizationsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapGroup("/Culture");

        group.MapGet("", (HttpContext context, [FromQuery] string id, [FromQuery] string returnUrl) =>
        {
            if (!id.IsNullOrEmpty())
            {
                context.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(id, id))
                );

                // if got login user, then save to db 

            }
            return TypedResults.LocalRedirect($"~/{returnUrl}");
        });

        return group;
    }
}
