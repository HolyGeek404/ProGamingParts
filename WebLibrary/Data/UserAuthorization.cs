using Microsoft.AspNetCore.Mvc.Filters;

namespace WebLibrary.Data;

internal class UserAuthorization : Attribute, IAuthorizationFilter 
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.Session.GetString("UserId");
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrEmpty(userId))
        {
            context.Result = new RedirectResult("/User/LogIn");
        }
    }
}