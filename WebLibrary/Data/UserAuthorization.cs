using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace WebLibrary.Data;

internal class UserAuthorization : Attribute, IAuthorizationFilter 
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.Session.GetString("UserId");
        if (string.IsNullOrWhiteSpace(userId) || userId.IsNullOrEmpty())
        {
            context.Result = new RedirectResult("/User/LogIn");
        }
    }
}