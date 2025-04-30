using Microsoft.AspNetCore.Mvc.Filters;

namespace WebLibrary.Data;

public class AdminAuthorization : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userType = context.HttpContext.Session.GetString("Type");
        if (!string.Equals(userType, "Admin"))
        {
            context.Result = new BadRequestResult();
        }
    }
}